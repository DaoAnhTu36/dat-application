import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import {
  PageingReq,
  StatusCodeApiResponse,
} from '../../../../../commons/const/ConstStatusCode';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import {
  GoodsDetailWhModel,
  WarehouseService,
} from '../../../../../services/warehouse-service.service';
import { Router } from '@angular/router';
import { CommonModule, NgFor, NgForOf } from '@angular/common';
import { UrlConstEnum } from '../../../../../menu/config-url';
import { FormsModule } from '@angular/forms';
import { CustomCurrencyPipe } from '../../../../../commons/pipes/custom-currency.pipe';

@Component({
  selector: 'app-product-index',
  standalone: true,
  imports: [NgForOf, CommonModule, FormsModule],
  templateUrl: './product-index.component.html',
  styleUrl: './product-index.component.scss',
})
export class ProductIndexComponent {
  data: GoodsDetailWhModel[] = [];
  currentPage: number = 0;
  totalPage: number = 0;
  pageNumber: number[] = [];
  pageNumberSelect: number = 0;
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
        this.pageNumber = [];
        this.currentPage = res.pageInfo?.currentPage ?? 0;
        this.totalPage = res.pageInfo?.totalPage ?? 0;
        for (let index = 0; index < this.totalPage; index++) {
          this.pageNumber.push(index);
        }
        this._loadingService.hide();
      });
  }

  onAsyncData() {
    this.list();
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

  onFilter(pageNumber: number = PageingReq.PAGE_NUMBER) {
    this._loadingService.show();
    let req = {
      pageNumber: pageNumber,
      pageSize: PageingReq.PAGE_SIZE,
    };
    this._warehouseService.goodsList(req).subscribe((res) => {
      this._loadingService.hide();
      if (
        res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS &&
        res.isNormal
      ) {
        this.pageNumber = [];
        this.currentPage = res.pageInfo?.currentPage ?? 0;
        this.totalPage = res.pageInfo?.totalPage ?? 0;
        for (let index = 0; index < this.totalPage; index++) {
          this.pageNumber.push(index);
        }
        this.data = res.data?.list || [];
      } else {
      }
    });
  }

  onPage(index: number, type: string) {
    if (type === 'previous') {
      this.currentPage -= 1;
      this.onFilter(this.currentPage);
    } else if (type === 'next') {
      this.currentPage += 1;
      this.onFilter(this.currentPage);
    } else {
      this.currentPage = index;
      this.onFilter(this.currentPage);
    }
    this.pageNumberSelect = this.currentPage;
  }

  onPageSelect(event: any) {
    this.currentPage = event.target.value;
    this.onFilter(this.currentPage);
    this.pageNumberSelect = this.currentPage;
  }
}
