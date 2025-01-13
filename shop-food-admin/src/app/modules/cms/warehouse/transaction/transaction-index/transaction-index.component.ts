import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import {
  TransactionWhModel,
  WarehouseService,
} from '../../../../../services/warehouse-service.service';
import { Router } from '@angular/router';
import {
  PageingReq,
  StatusCodeApiResponse,
} from '../../../../../commons/const/ConstStatusCode';
import { CommonModule, NgForOf } from '@angular/common';
import { CommonServiceService } from '../../../../../services/common-service.service';
import { CustomCurrencyPipe } from '../../../../../commons/pipes/custom-currency.pipe';
import { CustomDatePipe } from '../../../../../commons/pipes/custom-date.pipe';
import { UrlConstEnum } from '../../../../../menu/config-url';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-transaction-index',
  standalone: true,
  imports: [
    NgForOf,
    CustomCurrencyPipe,
    CustomDatePipe,
    CommonModule,
    FormsModule,
  ],
  templateUrl: './transaction-index.component.html',
  styleUrl: './transaction-index.component.scss',
})
export class TransactionIndexComponent {
  transactionCode: string = '';
  datetimeAfter: Date = new Date();
  datetimeBefore: Date = new Date();
  stockId: string = '';
  transactionType: string = '';
  data: any[] = [];
  currentPage: number = 0;
  totalPage: number = 0;
  pageNumber: number[] = [];
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

  add() {
    this.router.navigate([UrlConstEnum.TRANSACTION_CREATE]);
  }

  edit(id: string | undefined) {
    this.router.navigate([UrlConstEnum.TRANSACTION_UPDATE, id]);
  }

  detail(id: string | undefined) {
    this.router.navigate([UrlConstEnum.TRANSACTION_DETAIL, id]);
  }

  delete(id: string | undefined) {
    this.router.navigate([UrlConstEnum.TRANSACTION_DELETE, id]);
  }

  list(pageNumber: number, pageSize: number = PageingReq.PAGE_SIZE) {
    this._loadingService.show();
    this._warehouseService
      .transactionList({
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
          this.data =
            res.data?.list?.map((rs) => {
              return {
                id: rs.id,
                transactionCode: rs.transactionCode,
                transactionDate: rs.transactionDate,
                transactionType: rs.transactionType === '0' ? 'Nhập' : 'Xuất',
                totalPrice: rs.totalPrice,
                stockName: rs.stockName,
              };
            }) ?? [];
        } else {
        }
      });
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
}
