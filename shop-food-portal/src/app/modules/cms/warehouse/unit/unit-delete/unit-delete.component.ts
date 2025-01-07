import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import { WarehouseService } from '../../../../../services/warehouse-service.service';
import { StatusCodeApiResponse } from '../../../../../commons/const/ConstStatusCode';
import { UrlConstEnum } from '../../../../../menu/config-url';

@Component({
  selector: 'app-unit-delete',
  standalone: true,
  imports: [],
  templateUrl: './unit-delete.component.html',
  styleUrl: './unit-delete.component.scss',
})
export class UnitDeleteComponent {
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _loadingService: LoadingService,
    private readonly _router: Router
  ) {}

  ngOnInit() {
    this.delete();
  }

  delete() {
    this._loadingService.show();
    const id = this._activatedRoute.snapshot.params['id'];
    this._warehouseService
      .deleteUnit({
        id: id,
      })
      .subscribe((res) => {
        this._loadingService.hide();
        if (
          res.isNormal &&
          res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS
        ) {
          this._router.navigate([UrlConstEnum.UNIT_INDEX]);
        }
      });
  }
}
