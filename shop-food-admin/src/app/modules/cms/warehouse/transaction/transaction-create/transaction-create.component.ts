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
  SubTransactionWhCreateModelReq,
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
  categories: CategoryWhModel[] | null | undefined;
  units: UnitWhModel[] | null | undefined;
  myForm: FormGroup;
  transactionCode = new FormControl('');
  transactionDate = new FormControl('');
  supplierId = new FormControl('');
  listTransDetail: any[] = [];
  totalPriceDetail = new FormControl('');
  transactionType = new FormControl('');
  listTransactionType = [
    { id: '0', name: 'Nhập' },
    { id: '1', name: 'Xuất' },
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
    this.listCategory();
    this.listUnit();
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

  listCategory() {
    this._warehouseService
      .categoryList({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE,
      })
      .subscribe((res) => {
        if (res.isNormal) {
          this.categories = res.data?.list;
        }
      });
  }

  get items(): FormArray {
    return this.myForm.get('items') as FormArray;
  }

  addItem() {
    const itemFormGroup = this.fb.group({
      productId: [''],
      goodsCode: [''],
      productName: [''],
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
            productId: res.data?.id,
            goodsCode: res.data?.goodsCode,
            productName: res.data?.name,
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
    let productId = this.myForm.value['items'][i]['productId'];
    let productName = this.myForm.value['items'][i]['productName'];
    let unitId = this.myForm.value['items'][i]['unitId'];
    let supplierId = this.myForm.value['items'][i]['supplierId'];
    let index = this.listTransDetail.findIndex((x) => x.goodsCode == goodsCode);
    if (index >= 0) {
      this.listTransDetail.splice(i, 1);
    }
    this.listTransDetail.splice(index, 0, {
      productId: productId,
      goodsCode: goodsCode,
      productName: productName,
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
