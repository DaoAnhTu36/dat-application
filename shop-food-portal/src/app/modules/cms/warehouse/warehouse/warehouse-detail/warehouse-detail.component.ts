import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { StatusCodeApiResponse } from '../../../../../commons/const/ConstStatusCode';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import { WarehouseService } from '../../../../../services/warehouse-service.service';

@Component({
  selector: 'app-warehouse-detail',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './warehouse-detail.component.html',
  styleUrl: './warehouse-detail.component.scss'
})
export class WarehouseDetailComponent {

  constructor(private readonly _warehouseService: WarehouseService
    , private readonly _activatedRoute: ActivatedRoute
    , private readonly _loadingService: LoadingService
    , private readonly _router: Router
  ) {

  }
  name = '';
  address = '';
  createdBy = '';
  createdDate = '';
  updatedBy = '';
  updatedDate = '';
  ngOnInit() {
    this.getDetail();
  }

  getDetail() {
    this._loadingService.show();
    const id = this._activatedRoute.snapshot.params['id'];
    this._warehouseService.detailWarehouse({ id: id }).subscribe(res => {
      if (res.isNormal && res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS) {
        this.name = res.data?.name ?? "";
        this.address = res.data?.address ?? "";
        this.createdBy = res.data?.createdBy ?? "";
        this.createdDate = res.data?.createdDate.toLocaleString() ?? "";
        this.updatedBy = res.data?.updatedBy ?? "";
        this.updatedDate = res.data?.updatedDate.toLocaleString() ?? "";
      }
      this._loadingService.hide();
    })
  }
}
