import { NgFor, NgIf } from '@angular/common';
import { Component, ElementRef, Renderer2 } from '@angular/core';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  FormArray,
  FormControl,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import {
  GoodsDetailWhModel,
  StockWhModel,
  SubTransactionWhCreateModelReq,
  SupplierModel,
  TransactionWhCreateModelReq,
  UnitWhModel,
  WarehouseService,
} from '../../../../../services/warehouse-service.service';
import {
  PageingReq,
  StatusCodeApiResponse,
} from '../../../../../commons/const/ConstStatusCode';
import { CommonServiceService } from '../../../../../services/common-service.service';
import { UrlConstEnum } from '../../../../../menu/config-url';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import * as jquery from 'jquery';

@Component({
  selector: 'app-transaction-create',
  standalone: true,
  imports: [NgFor, ReactiveFormsModule, NgIf],
  templateUrl: './transaction-create.component.html',
  styleUrl: './transaction-create.component.scss',
})
export class TransactionCreateComponent {
  goods: GoodsDetailWhModel[] = [];
  stocks: StockWhModel[] = [];
  suppliers: SupplierModel[] = [];
  units: UnitWhModel[] = [];
  myForm: FormGroup;
  transactionCode = new FormControl('');
  transactionDate = new FormControl('');
  supplierId = new FormControl('');
  listTransDetail: any[] = [];
  totalPriceDetail = new FormControl('');
  transactionType = new FormControl('0');
  stockId = new FormControl('');
  listTransactionType = [
    { id: '0', name: 'Nhập' },
    // { id: '1', name: 'Xuất' },
  ];
  ClickedOrEntered = false;

  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly _router: Router,
    private readonly _toastService: ToastrService,
    private readonly _renderer: Renderer2,
    private readonly _el: ElementRef,
    private readonly fb: FormBuilder,
    private readonly _commonService: CommonServiceService,
    private readonly _loadingService: LoadingService
  ) {
    this.myForm = this.fb.group({
      items: this.fb.array([]),
    });
    let currentDatetime = new Date().getTime();
    this.transactionCode = new FormControl(`N${currentDatetime.toString()}`);
    this.transactionDate.setValue(new Date().toISOString().split('T')[0]);
  }

  ngOnInit(): void {
    this.listSupplier();
    this.listStock();
    this.listGoods();
    this.listUnit();
  }

  listGoods() {
    this._warehouseService
      .goodsList({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE_SEARCH,
      })
      .subscribe((res) => {
        if (res.isNormal) {
          this.goods = res.data?.list ?? [];
        }
      });
  }

  listStock() {
    this._warehouseService
      .stockList({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE_SEARCH,
      })
      .subscribe((res) => {
        if (res.isNormal) {
          this.stocks = res.data?.list ?? [];
          this.stockId.setValue(this.stocks[0].id ?? '');
        }
      });
  }

  listUnit() {
    this._warehouseService
      .unitList({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE_SEARCH,
      })
      .subscribe((res) => {
        if (res.isNormal) {
          this.units = res.data?.list ?? [];
        }
      });
  }

  listSupplier() {
    this._warehouseService
      .supplierList({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE_SEARCH,
      })
      .subscribe((res) => {
        if (res.isNormal) {
          this.suppliers = res.data?.list ?? [];
        }
      });
  }

  get items(): FormArray {
    return this.myForm.get('items') as FormArray;
  }

  addItem() {
    const itemFormGroup = this.fb.group({
      goodsId: [''],
      goodsCode: [''],
      goodsName: [''],
      unitId: [''],
      supplierId: [''],
      quantity: [''],
      unitPrice: [''],
      totalAmount: [''],
      dateOfManufacture: [''],
      dateOfExpired: [''],
    });

    this.items.push(itemFormGroup);
  }

  removeItem(index: number) {
    this.items.removeAt(index);
    this.listTransDetail.splice(index);
  }

  onRewrite(index: any, name: any, isId: boolean = false) {
    return isId ? `${name}_${index}_id` : `${name}_${index}_name`;
  }

  onCancel() {
    this._router.navigate([UrlConstEnum.TRANSACTION_INDEX]);
  }

  onSearchProduct(i: any) {
    const goodsCode = this.myForm.value['items'][i]['goodsCode'];
    // const goodsCode = this.goods?.find(
    //   (x) => x.name == this.myForm.value['items'][i]['goodsCode']
    // )?.goodsCode;
    let index = i;
    this._warehouseService
      .goodsDetail({
        goodsCode: goodsCode,
      })
      .subscribe((res) => {
        if (
          res.isNormal &&
          res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
        ) {
          if (index >= 0) {
            this.listTransDetail.splice(i, 1);
          }
          this.listTransDetail.push({
            goodsId: res.data?.id,
            goodsCode: res.data?.goodsCode,
            goodsName: res.data?.name,
            unitId: '',
            supplierId: '',
            quantity: 1,
            unitPrice: res.data?.price,
            totalAmount: 0,
            dateOfManufacture: null,
            dateOfExpired: null,
          });
          this.items.setValue(this.listTransDetail);
          this.calTotalAmountDetail();
          // this.addItem();
        } else {
          this._toastService.error('Không tìm thấy sản phẩm');
          if (this.listTransDetail.length >= i) {
            this.listTransDetail.splice(i, 1);
            this.removeItem(i);
          }
        }
      });
  }

  onCalTotalAmount(i: any) {
    let quantity = this._commonService.revertFormatCurrency(
      this.myForm.value['items'][i]['quantity']
    );
    let unitPrice = this._commonService.revertFormatCurrency(
      this.myForm.value['items'][i]['unitPrice']
    );
    let totalAmount = quantity * unitPrice;
    let goodsCode = this.myForm.value['items'][i]['goodsCode'];
    let goodsId = '';
    let goodsName = this.myForm.value['items'][i]['goodsName'];
    let unitId = this.myForm.value['items'][i]['unitId'];
    let supplierId = this.myForm.value['items'][i]['supplierId'];
    let index = this.listTransDetail.findIndex((x) => x.goodsCode == goodsCode);
    let dateOfManufacture = this.myForm.value['items'][i]['dateOfManufacture'];
    let dateOfExpired = this.myForm.value['items'][i]['dateOfExpired'];
    if (index >= 0) {
      goodsId = this.listTransDetail[index].goodsId;
      this.listTransDetail.splice(index, 1, {
        goodsId: goodsId,
        goodsCode: goodsCode,
        goodsName: goodsName,
        unitId: unitId,
        supplierId: supplierId,
        quantity: this._commonService.formatNumber(quantity),
        unitPrice: this._commonService.formatNumber(unitPrice),
        totalAmount: this._commonService.formatNumber(totalAmount),
        dateOfManufacture: dateOfManufacture,
        dateOfExpired: dateOfExpired,
      });
      console.log(this.listTransDetail);
      this.items.setValue(this.listTransDetail);
      this.calTotalAmountDetail();
    }
  }

  calTotalAmountDetail() {
    let totalPriceDetail = 0;
    this.listTransDetail.forEach((element: any, index: any) => {
      totalPriceDetail += this._commonService.revertFormatCurrency(
        element.totalAmount
      );
    });
    this.totalPriceDetail.setValue(
      this._commonService.formatCurrency(totalPriceDetail)
    );
  }

  onSearchStock() {
    var searchReq = this.stockId.value;
    this._warehouseService
      .stockSearch({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE_SEARCH,
        textSearch: searchReq,
      })
      .subscribe((res) => {
        if (
          res.isNormal &&
          res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS
        ) {
          this.stocks = res.data?.list ?? [];
        } else {
          this.listStock();
        }
      });
  }

  onSearchGoods(i: any) {
    const searchReq = this.myForm.value['items'][i]['goodsCode'];
    this._warehouseService
      .goodsSearch({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE_SEARCH,
        textSearch: searchReq,
      })
      .subscribe((res) => {
        if (
          res.isNormal &&
          res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS
        ) {
          this.goods = res.data?.list ?? [];
        } else {
          this.listGoods();
        }
      });
  }

  onSearch() {
    console.log(1);
  }

  onSetProperties(i: any, event: any, type: number) {
    const valueText = (event.target as HTMLSelectElement).value;
    let item = this.listTransDetail[i];
    if (type === 1) {
      //set value supplier
      item.supplierId = this.suppliers?.find((x) => x.name === valueText)?.name;
    } else if (type === 2) {
      //set value unit
      item.unitId = this.units?.find((x) => x.name === valueText)?.name;
    }
    this.listTransDetail.splice(i, 1, item);
    this.items.setValue(this.listTransDetail);
    this.calTotalAmountDetail();
  }

  onSave() {
    try {
      this._loadingService.show();
      let stockId = this.stockId.value ?? '';
      let details: SubTransactionWhCreateModelReq[] = [];
      this.myForm.value['items'].forEach((element: any) => {
        details.push({
          quantity: this._commonService.revertFormatCurrency(element.quantity),
          unitPrice: this._commonService.revertFormatCurrency(
            element.unitPrice
          ),
          totalPrice: this._commonService.revertFormatCurrency(
            element.totalAmount
          ),
          goodsId: element.goodsId,
          dateOfExpired: element.dateOfExpired,
          dateOfManufacture: element.dateOfManufacture,
          supplierId:
            this.suppliers?.find((x) => x.name === element.supplierId)?.id ??
            this.suppliers[0].id,
          unitId: this.units?.find((x) => x.name === element.unitId)?.id,
        });
      });
      let request: TransactionWhCreateModelReq = {
        transactionDate: new Date(this.transactionDate.value ?? '') ?? null,
        transactionCode: this.transactionCode.value ?? null,
        transactionType: this.transactionType.value,
        totalPrice: this._commonService.revertFormatCurrency(
          `${this.totalPriceDetail.value}`
        ),
        stockId: stockId,
        details: details,
      };
      this._warehouseService.transactionCreate(request).subscribe((res) => {
        if (
          res.isNormal &&
          res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
        ) {
          this._router.navigate([UrlConstEnum.TRANSACTION_INDEX]);
        } else {
          this._toastService.error('Thất bại');
        }
      });
      this._loadingService.hide();
    } catch (error) {
      this._toastService.error('Thất bại');
      console.log('error :>> ', error);
      this._loadingService.hide();
    }
  }
}
