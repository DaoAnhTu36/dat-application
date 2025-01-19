import { Component } from '@angular/core';
import {
  GoodsRetailWhStatisticsModelReq,
  GoodsRetailWhStatisticsModelRes,
  WarehouseService,
} from '../../../services/warehouse-service.service';
import { NgFor, NgIf } from '@angular/common';
import { StatusCodeApiResponse } from '../../../commons/const/ConstStatusCode';
import { CustomCurrencyPipe } from '../../../commons/pipes/custom-currency.pipe';
import { CustomNumberPipe } from '../../../commons/pipes/custom-number.pipe';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { BaseChartDirective } from 'ng2-charts';

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
  dataStatistics: GoodsRetailWhStatisticsModelRes[];
  form: FormGroup;

  constructor(
    private readonly _warehouseService: WarehouseService,
    private fb: FormBuilder
  ) {
    this.dataStatistics = [];
    this.form = this.fb.group({
      fromDate: [''],
      toDate: [''],
    });
  }

  ngOnInit() {
    this.onGetStatistics();
  }

  onGetStatistics(req: GoodsRetailWhStatisticsModelReq = {}) {
    this._warehouseService.goodsretailStatistics(req).subscribe((res) => {
      if (
        res.isNormal &&
        res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
      ) {
        this.dataStatistics = res.data ?? [];
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

  public lineChartData = [
    { data: [65, 59, 80, 81, 56, 55, 40], label: 'Series A' },
    { data: [28, 48, 40, 19, 86, 27, 90], label: 'Series B' },
  ];

  // Chart Labels
  public lineChartLabels = [
    'January',
    'February',
    'March',
    'April',
    'May',
    'June',
    'July',
  ];

  // Chart Options
  public lineChartOptions = {
    responsive: true,
  };

  // Chart Colors
  public lineChartColors = [
    {
      backgroundColor: 'rgba(105,159,177,0.2)',
      borderColor: 'rgba(105,159,177,1)',
    },
    {
      backgroundColor: 'rgba(255,99,132,0.2)',
      borderColor: 'rgba(255,99,132,1)',
    },
  ];

  // Chart Type
  public lineChartType = 'line';
}
