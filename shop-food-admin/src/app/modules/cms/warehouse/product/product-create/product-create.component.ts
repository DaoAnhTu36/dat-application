import { Component } from '@angular/core';
import { ReactiveFormsModule, FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import {
  PageingReq,
  StatusCodeApiResponse,
} from '../../../../../commons/const/ConstStatusCode';
import {
  SupplierWhListModelRes,
  UnitWhListModelRes,
  WarehouseService,
} from '../../../../../services/warehouse-service.service';
import { Router } from '@angular/router';
import { NgFor } from '@angular/common';
import { UrlConstEnum } from '../../../../../menu/config-url';

@Component({
  selector: 'app-product-create',
  standalone: true,
  imports: [ReactiveFormsModule, NgFor],
  templateUrl: './product-create.component.html',
  styleUrl: './product-create.component.scss',
})
export class ProductCreateComponent {
  name = new FormControl('');
  description = new FormControl('');
  supplierId = new FormControl('');
  unitId = new FormControl('');
  barCode = new FormControl('');
  listUnit: UnitWhListModelRes = {};
  listSupplier: SupplierWhListModelRes = {};
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly _router: Router,
    private readonly _toastService: ToastrService
  ) {}

  ngOnInit() {
    this.getListUnit();
    this.getListSupplier();
  }

  getListUnit() {
    this._warehouseService
      .listUnit({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE,
      })
      .subscribe((res) => {
        if (
          res.isNormal &&
          res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
        ) {
          this.listUnit = res.data ?? {};
        }
      });
  }

  getListSupplier() {
    this._warehouseService
      .listSupplier({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE,
      })
      .subscribe((res) => {
        if (
          res.isNormal &&
          res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
        ) {
          this.listSupplier = res.data ?? {};
        }
      });
  }

  onCreate() {
    const nameValue = this.name.value ?? '';
    const descriptionValue = this.description.value ?? '';
    const supplierIdValue = this.supplierId.value ?? '';
    const unitIdValue = this.unitId.value ?? '';
    const barCodeValue = this.barCode.value ?? '';
    if (!nameValue || !supplierIdValue || !unitIdValue) {
      this._toastService.error('Thông tin không được bỏ trống');
      return;
    }
    var obj = {
      name: nameValue,
      description: descriptionValue,
      supplierId: supplierIdValue,
      unitId: unitIdValue,
      barCode: barCodeValue,
    };
    this._warehouseService.createProduct(obj).subscribe((res) => {
      if (
        res.isNormal &&
        res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
      ) {
        this._router.navigate([UrlConstEnum.PRODUCT_INDEX]);
      } else {
        this._toastService.error('Lưu thất bại');
      }
    });
  }
}
