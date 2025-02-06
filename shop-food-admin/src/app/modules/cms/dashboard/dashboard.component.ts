import { Component, QueryList, ViewChild, ViewChildren } from '@angular/core';
import {
  TransactionRetailWhStatisticsModelReq,
  TransactionRetailWhStatisticsModelRes,
  WarehouseService,
} from '../../../services/warehouse-service.service';
import { NgFor, NgIf } from '@angular/common';
import { StatusCodeApiResponse } from '../../../commons/const/ConstStatusCode';
import { CustomCurrencyPipe } from '../../../commons/pipes/custom-currency.pipe';
import { CustomNumberPipe } from '../../../commons/pipes/custom-number.pipe';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { BaseChartDirective } from 'ng2-charts';
import { ChartConfiguration, ChartData, ChartType } from 'chart.js';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    NgFor,
    CustomCurrencyPipe,
    CustomNumberPipe,
    ReactiveFormsModule,
    NgIf,
    BaseChartDirective,
  ],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent {
  @ViewChildren(BaseChartDirective) charts:
    | QueryList<BaseChartDirective>
    | undefined;
  dataStatistics: TransactionRetailWhStatisticsModelRes[];
  dataStatisticsTop5: TransactionRetailWhStatisticsModelRes[];
  form: FormGroup;
  isShowChartHistory: boolean = false;
  dataChartHistoryChange: any = {
    lineChartData: {
      datasets: [
        {
          data: [],
          label: 'Biểu đồ giá sản phẩm',
          backgroundColor: 'rgb(255, 164, 36)',
          borderColor: 'rgba(148,159,177,1)',
          pointBackgroundColor: 'rgba(148,159,177,1)',
          pointBorderColor: '#fff',
          pointHoverBackgroundColor: '#fff',
          pointHoverBorderColor: 'rgba(148,159,177,0.8)',
          fill: 'origin',
        },
      ],
      labels: [],
    },
    lineChartLabels: [],
    lineChartOptions: { responsive: true },
    lineChartColors: [
      {
        backgroundColor: 'rgba(105,159,177,0.2)',
        borderColor: 'rgba(105,159,177,1)',
      },
    ],
    lineChartType: 'line',
  };
  dataChartTop5Sale: any = {
    lineChartData: {
      datasets: [
        {
          data: [],
          label: 'Top 5 sản phẩm bán chạy nhất',
          backgroundColor: 'rgb(27, 255, 103)',
          borderColor: 'rgba(148,159,177,1)',
          pointBackgroundColor: 'rgba(148,159,177,1)',
          pointBorderColor: '#fff',
          pointHoverBackgroundColor: '#fff',
          pointHoverBorderColor: 'rgba(148,159,177,0.8)',
          fill: 'origin',
        },
      ],
      labels: [],
    },
    lineChartLabels: [],
    lineChartOptions: { responsive: true },
    lineChartColors: [
      {
        backgroundColor: 'rgba(105,159,177,0.2)',
        borderColor: 'rgba(105,159,177,1)',
      },
    ],
    lineChartType: 'bar',
  };

  constructor(
    private readonly _warehouseService: WarehouseService,
    private fb: FormBuilder
  ) {
    this.dataStatistics = [];
    this.dataStatisticsTop5 = [];
    this.form = this.fb.group({
      fromDate: [''],
      toDate: [''],
    });
  }

  ngOnInit() {
    this.onGetStatistics();
    this.onGetStatisticsTop5();
  }

  onGetStatistics(req: TransactionRetailWhStatisticsModelReq = {}) {
    this._warehouseService.transactionretailStatistics(req).subscribe((res) => {
      if (
        res.isNormal &&
        res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
      ) {
        this.dataStatistics = res.data ?? [];
      }
    });
  }

  onGetStatisticsTop5(req: TransactionRetailWhStatisticsModelReq = {}) {
    this._warehouseService
      .transactionretailStatisticsTop5(req)
      .subscribe((res) => {
        if (
          res.isNormal &&
          res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
        ) {
          let item: any[] = [];
          let lstLabel: any[] = [];
          this.dataStatisticsTop5 = res.data ?? [];
          if (this.dataStatisticsTop5?.length > 0) {
            this.dataStatisticsTop5.forEach((x) => {
              if (
                x.goodsName !== null &&
                x.goodsName !== undefined &&
                x.goodsName != ''
              ) {
                item.push(x.quantity);
                lstLabel.push(x.goodsName);
              }
            });
            this.dataChartTop5Sale.lineChartData.datasets[0].data = item;
            this.dataChartTop5Sale.lineChartData.labels = lstLabel;
          } else {
            this.isShowChartHistory = false;
          }
        } else {
          this.isShowChartHistory = false;
        }
      });
  }

  onViewChart(id: any) {
    this._warehouseService
      .goodsretailHistoryChangeOfPrice({
        goodsId: id,
      })
      .subscribe((res) => {
        if (
          res.isNormal &&
          res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS
        ) {
          this.isShowChartHistory = true;
          let item: any[] = [];
          let lstLabel: any[] = [];
          res.data?.prices?.forEach((e) => {
            item.push(e);
            lstLabel.push(e);
          });
          this.dataChartHistoryChange.lineChartData.datasets[0].data = item;
          this.dataChartHistoryChange.lineChartData.labels = lstLabel;
          this.onAutoRefreshChart();
        } else {
          this.isShowChartHistory = false;
        }
      });
  }

  onSubmit() {
    const fromDate = this.form.value.fromDate;
    const toDate = this.form.value.toDate;
    if (fromDate === '' && toDate === '') {
      return;
    }
    this.onGetStatistics({
      fromDate: new Date(fromDate),
      toDate: new Date(toDate),
    });
  }

  onAutoRefreshChart() {
    this.charts?.forEach((child) => {
      child?.chart?.update();
    });
  }
}
