import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { StatusCodeApiResponse } from '../../../../../commons/const/ConstStatusCode';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import {
  GoodsWhDetailModelRes,
  WarehouseService,
} from '../../../../../services/warehouse-service.service';
import { CustomDatePipe } from '../../../../../commons/pipes/custom-date.pipe';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CustomDatePipe],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.scss',
})
export class ProductDetailComponent {
  data: any;
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _loadingService: LoadingService
  ) {}

  ngOnInit() {
    this.getDetail();
  }

  getDetail() {
    this._loadingService.show();
    const id = this._activatedRoute.snapshot.params['id'];
    this._warehouseService.goodsDetail({ id: id }).subscribe((res) => {
      if (
        res.isNormal &&
        res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
      ) {
        this.data = res.data ?? {};
      }
      this._loadingService.hide();
    });
  }
}
