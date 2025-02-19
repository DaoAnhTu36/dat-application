import { Component } from '@angular/core';
import { NgFor } from '@angular/common';
import { Router } from '@angular/router';
import { PageingReq } from '../../../../../commons/const/ConstStatusCode';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import {
  StockWhListModelRes,
  WarehouseService,
} from '../../../../../services/warehouse-service.service';
import { ToastrService } from 'ngx-toastr';
import { UrlConstEnum } from '../../../../../menu/config-url';

@Component({
  selector: 'app-warehouse-index',
  standalone: true,
  imports: [NgFor],
  templateUrl: './warehouse-index.component.html',
  styleUrl: './warehouse-index.component.scss',
})
export class WarehouseIndexComponent {
  listWarehouse: StockWhListModelRes | undefined;
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
      .stockList({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE,
      })
      .subscribe((res) => {
        this.listWarehouse = res.data;
        this._loadingService.hide();
      });
  }

  add() {
    this.router.navigate([UrlConstEnum.STOCK_CREATE]);
  }

  edit(id: string | undefined) {
    this.router.navigate([UrlConstEnum.STOCK_UPDATE, id]);
  }

  detail(id: string | undefined) {
    this.router.navigate([UrlConstEnum.STOCK_DETAIL, id]);
  }

  delete(id: string | undefined) {
    this.router.navigate([UrlConstEnum.STOCK_DELETE, id]);
  }
}
