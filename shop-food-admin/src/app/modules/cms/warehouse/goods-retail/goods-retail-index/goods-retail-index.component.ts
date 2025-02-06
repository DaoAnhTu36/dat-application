import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import {
  PageingReq,
  StatusCodeApiResponse,
} from '../../../../../commons/const/ConstStatusCode';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import { CommonServiceService } from '../../../../../services/common-service.service';
import {
  GoodsRetailModelRes,
  GoodsRetailWhListModelRes,
  StockWhModel,
  WarehouseService,
} from '../../../../../services/warehouse-service.service';
import { Router } from '@angular/router';
import { UrlConstEnum } from '../../../../../menu/config-url';
import { NgForOf, CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CustomCurrencyPipe } from '../../../../../commons/pipes/custom-currency.pipe';
import { CustomDatePipe } from '../../../../../commons/pipes/custom-date.pipe';

@Component({
  selector: 'app-goods-retail-index',
  standalone: true,
  imports: [NgForOf, CustomCurrencyPipe, CommonModule, FormsModule],
  templateUrl: './goods-retail-index.component.html',
  styleUrl: './goods-retail-index.component.scss',
})
export class GoodsRetailIndexComponent {
  data: GoodsRetailWhListModelRes | undefined;
  currentPage: number = 0;
  totalPage: number = 0;
  pageNumber: number[] = [];
  stocks: StockWhModel[] | null | undefined;
  goodsCode: string | undefined;
  goodsName: string | undefined;
  goodsPrice: string | undefined;
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly router: Router,
    private readonly _loadingService: LoadingService,
    private readonly _toastService: ToastrService,
    private readonly _commonService: CommonServiceService
  ) {}

  ngOnInit(): void {
    this.list(PageingReq.PAGE_NUMBER);
  }

  edit(id: string | undefined) {
    this.router.navigate([UrlConstEnum.TRANSACTION_UPDATE, id]);
  }

  list(pageNumber: number, pageSize: number = PageingReq.PAGE_SIZE) {
    this._loadingService.show();
    this._warehouseService
      .goodsretailList({
        pageNumber: pageNumber,
        pageSize: pageSize,
      })
      .subscribe((res) => {
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
          this.data = res.data;
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
  }

  onClearFilter() {
    this.goodsCode = undefined;
    this.goodsName = undefined;
    this.goodsPrice = undefined;
    this.list(PageingReq.PAGE_NUMBER);
  }

  onAsyncData() {
    this.list(PageingReq.PAGE_NUMBER);
  }

  onFilter(pageNumber: number = PageingReq.PAGE_NUMBER) {
    this._loadingService.show();
    let req = {
      pageNumber: pageNumber,
      pageSize: PageingReq.PAGE_SIZE_SEARCH,
      goodsCode: this.goodsCode,
      goodsName: this.goodsName,
    };
    this._warehouseService.goodsretailList(req).subscribe((res) => {
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
        this.data = res.data;
      } else {
      }
    });
  }

  onUpdatePrice(event: Event, record: GoodsRetailModelRes) {
    this._loadingService.show();
    const indexGoodsRetail = parseInt(
      (event.target as HTMLSelectElement).value
    );
    const id = record?.goodsRetails?.[indexGoodsRetail].id;
    const price = record?.goodsRetails?.[indexGoodsRetail].price;
    this._warehouseService
      .goodsretailUpdate({
        id: id,
        price: price,
      })
      .subscribe((res) => {
        this._loadingService.hide();
        if (
          res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS &&
          res.isNormal
        ) {
          this.list(PageingReq.PAGE_NUMBER);
        } else {
        }
      });
  }

  onCreatePrice(event: Event, record: GoodsRetailModelRes) {
    this._loadingService.show();
    const price = parseInt((event.target as HTMLSelectElement).value);
    this._warehouseService
      .goodsretailCreate({
        listReq: [
          {
            goodsCode: record?.goodsCode,
            goodsId: record?.goodsRetails?.[0].goodsId,
            goodsName: record?.goodsRetails?.[0].goodsName,
            price: price,
            transDetailId: record?.goodsRetails?.[0].transDetailId,
          },
        ],
      })
      .subscribe((res) => {
        this._loadingService.hide();
        if (
          res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS &&
          res.isNormal
        ) {
          this.list(PageingReq.PAGE_NUMBER);
        } else {
        }
      });
  }

  onSearch() {
    const goodsCode = this.goodsCode;
    const goodsName = this.goodsName;
    if (goodsCode === '' || goodsName === '') {
      this.onClearFilter();
    } else {
      this._warehouseService
        .goodsretailSearch({ goodsCode: goodsCode, goodsName: goodsName })
        .subscribe((res) => {
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
            this.data = res.data;
          } else {
            console.log(res);
          }
        });
    }
  }
}
