import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StatusCodeApiResponse } from '../../../../../commons/const/ConstStatusCode';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import { WarehouseService } from '../../../../../services/warehouse-service.service';
import { UrlConstEnum } from '../../../../../menu/config-url';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-warehouse-delete',
  standalone: true,
  imports: [],
  templateUrl: './warehouse-delete.component.html',
  styleUrl: './warehouse-delete.component.scss',
})
export class WarehouseDeleteComponent {
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _loadingService: LoadingService,
    private readonly _router: Router,
    private readonly _toastService: ToastrService
  ) {}

  ngOnInit() {
    this.delete();
  }

  delete() {
    this._loadingService.show();
    const id = this._activatedRoute.snapshot.params['id'];
    this._warehouseService
      .stockDelete({
        id: id,
      })
      .subscribe((res) => {
        this._loadingService.hide();
        if (
          res.isNormal &&
          res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS
        ) {
          this._router.navigate([UrlConstEnum.STOCK_DELETE]);
        } else {
          this._toastService.error('Thất bại');
        }
      });
  }
}
