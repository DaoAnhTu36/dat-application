import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { PageingReq } from '../../../../../commons/const/ConstStatusCode';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import {
  UnitWhModel,
  WarehouseService,
} from '../../../../../services/warehouse-service.service';
import { Router } from '@angular/router';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { UrlConstEnum } from '../../../../../menu/config-url';

@Component({
  selector: 'app-unit-index',
  standalone: true,
  imports: [NgFor, CommonModule, NgIf],
  templateUrl: './unit-index.component.html',
  styleUrl: './unit-index.component.scss',
})
export class UnitIndexComponent {
  data: UnitWhModel[] = [];
  currentPage: number = 0;
  totalPage: number = 0;
  pageNumber: number[] = [];
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly router: Router,
    private readonly _loadingService: LoadingService,
    private readonly _toastService: ToastrService
  ) {}

  ngOnInit() {
    this.list(PageingReq.PAGE_NUMBER);
  }

  OnPage(index: number, type: string) {
    if (type === 'previous') {
      this.currentPage -= 1;
      this.list(this.currentPage);
    } else if (type === 'next') {
      this.currentPage += 1;
      this.list(this.currentPage);
    } else {
      this.currentPage = index;
      this.list(this.currentPage);
    }
  }

  list(pageNumber: number, pageSize: number = PageingReq.PAGE_SIZE) {
    this._loadingService.show();
    this._warehouseService
      .unitList({
        pageNumber: pageNumber,
        pageSize: pageSize,
      })
      .subscribe((res) => {
        this.pageNumber = [];
        this.data = res.data?.list || [];
        this.currentPage = res.pageInfo?.currentPage ?? 0;
        this.totalPage = res.pageInfo?.totalPage ?? 0;
        for (let index = 0; index < this.totalPage; index++) {
          this.pageNumber.push(index);
        }
        this._loadingService.hide();
      });
  }

  add() {
    this.router.navigate([UrlConstEnum.UNIT_CREATE]);
  }

  edit(id: string | undefined) {
    this.router.navigate([UrlConstEnum.UNIT_UPDATE, id]);
  }

  detail(id: string | undefined) {
    this.router.navigate([UrlConstEnum.UNIT_DETAIL, id]);
  }

  delete(id: string | undefined) {
    this.router.navigate([UrlConstEnum.UNIT_DELETE, id]);
  }
}
