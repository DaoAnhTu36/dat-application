import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { PageingReq } from '../../../../../commons/const/ConstStatusCode';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import {
  GoodsDetailWhModel,
  WarehouseService,
} from '../../../../../services/warehouse-service.service';
import { Router } from '@angular/router';
import { NgFor } from '@angular/common';
import { UrlConstEnum } from '../../../../../menu/config-url';

@Component({
  selector: 'app-product-index',
  standalone: true,
  imports: [NgFor],
  templateUrl: './product-index.component.html',
  styleUrl: './product-index.component.scss',
})
export class ProductIndexComponent {
  data: GoodsDetailWhModel[] = [];
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly router: Router,
    private readonly _loadingService: LoadingService,
    private readonly _toastService: ToastrService
  ) {}

  ngOnInit() {
    this.list();
  }

  list() {
    this._loadingService.show();
    this._warehouseService
      .goodsList({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE,
      })
      .subscribe((res) => {
        this.data = res.data?.list || [];
        this._loadingService.hide();
      });
  }

  add() {
    this.router.navigate([UrlConstEnum.GOODS_CREATE]);
  }

  edit(id: string | undefined) {
    this.router.navigate([UrlConstEnum.GOODS_UPDATE, id]);
  }

  detail(id: string | undefined) {
    this.router.navigate([UrlConstEnum.GOODS_DETAIL, id]);
  }

  delete(id: string | undefined) {
    this.router.navigate([UrlConstEnum.GOODS_DELETE, id]);
  }
}
