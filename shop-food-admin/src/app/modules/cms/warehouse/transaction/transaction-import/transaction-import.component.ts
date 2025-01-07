import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import {
  PageingReq,
  StatusCodeApiResponse,
} from '../../../../../commons/const/ConstStatusCode';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import { CommonServiceService } from '../../../../../services/common-service.service';
import { WarehouseService } from '../../../../../services/warehouse-service.service';
import { Router } from '@angular/router';
import { NgForOf } from '@angular/common';
import { CustomCurrencyPipe } from '../../../../../commons/pipes/custom-currency.pipe';
import { CustomDatePipe } from '../../../../../commons/pipes/custom-date.pipe';
import { UrlConstEnum } from '../../../../../menu/config-url';

@Component({
  selector: 'app-transaction-import',
  standalone: true,
  imports: [NgForOf, CustomCurrencyPipe, CustomDatePipe],
  templateUrl: './transaction-import.component.html',
  styleUrl: './transaction-import.component.scss',
})
export class TransactionImportComponent {
  data: any[] = [];
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly router: Router,
    private readonly _loadingService: LoadingService,
    private readonly _toastService: ToastrService,
    private readonly _commonService: CommonServiceService
  ) {}

  ngOnInit(): void {
    this.listTransaction();
  }

  listTransaction() {
    this._loadingService.show();
    this._warehouseService
      .listTransaction({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE,
        transactionType: '0',
      })
      .subscribe((res) => {
        this._loadingService.hide();
        if (
          res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS &&
          res.isNormal
        ) {
          this.data =
            res.data?.list?.map((rs) => {
              return {
                id: rs.id,
                transactionCode: rs.transactionCode,
                transactionDate: rs.transactionDate,
                transactionType: rs.transactionType === '0' ? 'Nhập' : 'Xuất',
                totalPrice: rs.totalPrice,
              };
            }) ?? [];
        } else {
          this._toastService.error('Tải dữ liệu xảy ra lỗi.');
        }
      });
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
}
