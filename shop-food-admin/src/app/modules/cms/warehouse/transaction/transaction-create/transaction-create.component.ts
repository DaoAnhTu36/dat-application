import { NgFor, NgIf } from '@angular/common';
import { Component, ElementRef, Renderer2 } from '@angular/core';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  FormArray,
  Validators,
  FormControl,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import {
  CategoryWhListModelRes,
  CategoryWhModel,
  StockWhListModelRes,
  StockWhModel,
  SubTransactionWhCreateModelReq,
  SupplierModel,
  SupplierWhListModelRes,
  TransactionWhCreateModelReq,
  UnitWhListModelRes,
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

@Component({
  selector: 'app-transaction-create',
  standalone: true,
  imports: [NgFor, ReactiveFormsModule, NgIf],
  templateUrl: './transaction-create.component.html',
  styleUrl: './transaction-create.component.scss',
})
export class TransactionCreateComponent {
  stocks: StockWhModel[] | null | undefined;
  suppliers: SupplierModel[] | null | undefined;
  units: UnitWhModel[] | null | undefined;
  myForm: FormGroup;
  transactionCode = new FormControl('');
  transactionDate = new FormControl('');
  supplierId = new FormControl('');
  listTransDetail: any[] = [];
  totalPriceDetail = new FormControl('');
  transactionType = new FormControl('');
  stockId = new FormControl('');
  listTransactionType = [
    { id: '0', name: 'Nhập' },
    // { id: '1', name: 'Xuất' },
  ];

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
  }

  ngOnInit(): void {
    this.listSupplier();
    this.listUnit();
    this.listStock();
  }

  listStock() {
    this._warehouseService
      .stockList({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE,
      })
      .subscribe((res) => {
        if (res.isNormal) {
          this.stocks = res.data?.list;
        }
      });
  }

  listUnit() {
    this._warehouseService
      .unitList({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE,
      })
      .subscribe((res) => {
        if (res.isNormal) {
          this.units = res.data?.list;
        }
      });
  }

  listSupplier() {
    this._warehouseService
      .supplierList({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE,
      })
      .subscribe((res) => {
        if (res.isNormal) {
          this.suppliers = res.data?.list;
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
    let index = this.listTransDetail.findIndex((x) => x.goodsCode == goodsCode);
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
            unitPrice: this._commonService.formatCurrency(0),
            totalAmount: this._commonService.formatCurrency(1 * 0),
          });
          this.items.setValue(this.listTransDetail);
          this.calTotalAmountDetail();
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
    let quantity = this.myForm.value['items'][i]['quantity'];
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
    if (index >= 0) {
      goodsId = this.listTransDetail[index].goodsId;
      this.listTransDetail.splice(i, 1);
    }
    this.listTransDetail.splice(index, 0, {
      goodsId: goodsId,
      goodsCode: goodsCode,
      goodsName: goodsName,
      unitId: unitId,
      supplierId: supplierId,
      quantity: quantity,
      unitPrice: this._commonService.formatCurrency(unitPrice),
      totalAmount: this._commonService.formatCurrency(totalAmount),
    });
    this.items.setValue(this.listTransDetail);
    this.calTotalAmountDetail();
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

  onSave() {
    this._loadingService.show();
    let details: SubTransactionWhCreateModelReq[] = [];
    this.myForm.value['items'].forEach((element: any) => {
      details.push({
        quantity: this._commonService.parseStringToInt(element.quantity),
        unitPrice: this._commonService.revertFormatCurrency(element.unitPrice),
        totalPrice: this._commonService.revertFormatCurrency(
          element.totalAmount
        ),
        goodsId: element.goodsId,
        dateOfExpired: null,
        dateOfManufacture: null,
        supplierId: element.supplierId,
        unitId: element.unitId,
      });
    });
    let request: TransactionWhCreateModelReq = {
      transactionDate: new Date(this.transactionDate.value ?? '') ?? null,
      transactionCode: this.transactionCode.value ?? null,
      transactionType: this.transactionType.value,
      totalPrice: this._commonService.revertFormatCurrency(
        this.totalPriceDetail.value
      ),
      stockId: this.stockId.value ?? '',
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
  }
}
