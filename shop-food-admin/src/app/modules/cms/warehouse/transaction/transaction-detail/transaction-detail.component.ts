import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import { CommonServiceService } from '../../../../../services/common-service.service';
import {
  TransactionWhDetailModelRes,
  WarehouseService,
} from '../../../../../services/warehouse-service.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NgFor } from '@angular/common';
import { StatusCodeApiResponse } from '../../../../../commons/const/ConstStatusCode';
import { CustomCurrencyPipe } from '../../../../../commons/pipes/custom-currency.pipe';
import { CustomNumberPipe } from '../../../../../commons/pipes/custom-number.pipe';
import { CustomDatePipe } from '../../../../../commons/pipes/custom-date.pipe';

@Component({
  selector: 'app-transaction-detail',
  standalone: true,
  imports: [NgFor, CustomCurrencyPipe, CustomNumberPipe, CustomDatePipe],
  templateUrl: './transaction-detail.component.html',
  styleUrl: './transaction-detail.component.scss',
})
export class TransactionDetailComponent {
  data: any;
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly router: Router,
    private readonly _loadingService: LoadingService,
    private readonly _toastService: ToastrService,
    private readonly _commonService: CommonServiceService,
    private readonly _activatedRoute: ActivatedRoute
  ) {}

  ngOnInit() {
    this.getTransactionDetail();
  }
  getTransactionDetail() {
    this._loadingService.show();
    const id = this._activatedRoute.snapshot.params['id'];
    this._warehouseService
      .transactionDetail({
        id: id,
      })
      .subscribe((res) => {
        if (
          res.isNormal &&
          res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
        ) {
          this.data = res.data ?? {};
        } else {
          this._toastService.error('Error');
        }
        this._loadingService.hide();
      });
  }
}
