import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import {
  SupplierModel,
  WarehouseService,
} from '../../../../../services/warehouse-service.service';
import { Router } from '@angular/router';
import { PageingReq } from '../../../../../commons/const/ConstStatusCode';
import { NgFor } from '@angular/common';
import { UrlConstEnum } from '../../../../../menu/config-url';

@Component({
  selector: 'app-supplier-index',
  standalone: true,
  imports: [NgFor],
  templateUrl: './supplier-index.component.html',
  styleUrl: './supplier-index.component.scss',
})
export class SupplierIndexComponent {
  data: SupplierModel[] = [];
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
      .supplierList({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE,
      })
      .subscribe((res) => {
        this.data = res.data?.list || [];
        this._loadingService.hide();
      });
  }

  add() {
    this.router.navigate([UrlConstEnum.SUPPLIER_CREATE]);
  }

  edit(id: string | undefined) {
    this.router.navigate([UrlConstEnum.SUPPLIER_UPDATE, id]);
  }

  detail(id: string | undefined) {
    this.router.navigate([UrlConstEnum.SUPPLIER_DETAIL, id]);
  }

  delete(id: string | undefined) {
    this.router.navigate([UrlConstEnum.SUPPLIER_DELETE, id]);
  }
}
