import { Component } from '@angular/core';
import { ReactiveFormsModule, FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import {
  PageingReq,
  StatusCodeApiResponse,
} from '../../../../../commons/const/ConstStatusCode';
import {
  CategoryWhListModelRes,
  CategoryWhModel,
  GoodsWhCreateModelReq,
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
  goodsCode = new FormControl('');
  categoryId = new FormControl('');
  categories: CategoryWhModel[] = [];
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly _router: Router,
    private readonly _toastService: ToastrService
  ) {}

  ngOnInit() {
    this.listCategory();
  }

  onCreate() {
    var obj: GoodsWhCreateModelReq = {
      name: this.name.value ?? '',
      description: this.description.value ?? '',
      goodsCode: this.goodsCode.value ?? '',
      categoryId: this.categoryId.value ?? '',
    };
    this._warehouseService.goodsCreate(obj).subscribe((res) => {
      if (
        res.isNormal &&
        res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
      ) {
        this._router.navigate([UrlConstEnum.GOODS_INDEX]);
      } else {
        this._toastService.error('Thất bại');
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
        if (
          res.isNormal &&
          res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
        ) {
          this.categories = res.data?.list ?? [];
        }
      });
  }
}
