import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { PageingReq } from '../../../../../commons/const/ConstStatusCode';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import { UrlConstEnum } from '../../../../../menu/config-url';
import {
  CategoryWhModel,
  WarehouseService,
} from '../../../../../services/warehouse-service.service';
import { Router } from '@angular/router';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-category-index',
  standalone: true,
  imports: [NgFor],
  templateUrl: './category-index.component.html',
  styleUrl: './category-index.component.scss',
})
export class CategoryIndexComponent {
  data: CategoryWhModel[] = [];
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
      .categoryList({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE,
      })
      .subscribe((res) => {
        this.data = res.data?.list || [];
        this._loadingService.hide();
      });
  }

  add() {
    this.router.navigate([UrlConstEnum.CATEGORY_CREATE]);
  }

  edit(id: string | undefined) {
    this.router.navigate([UrlConstEnum.CATEGORY_UPDATE, id]);
  }

  detail(id: string | undefined) {
    this.router.navigate([UrlConstEnum.CATEGORY_DETAIL, id]);
  }

  delete(id: string | undefined) {
    this.router.navigate([UrlConstEnum.CATEGORY_DELETE, id]);
  }
}
