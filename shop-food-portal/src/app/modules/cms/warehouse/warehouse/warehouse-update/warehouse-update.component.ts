import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { WarehouseService } from '../../../../../services/warehouse-service.service';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import { StatusCodeApiResponse } from '../../../../../commons/const/ConstStatusCode';
import { UrlConstEnum } from '../../../../../menu/config-url';

@Component({
  selector: 'app-warehouse-update',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './warehouse-update.component.html',
  styleUrl: './warehouse-update.component.scss',
})
export class WarehouseUpdateComponent {
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _loadingService: LoadingService,
    private readonly _router: Router
  ) {}
  name = new FormControl('');
  address = new FormControl('');

  ngOnInit() {
    this.getDetailById();
  }

  getDetailById() {
    this._loadingService.show();
    const id = this._activatedRoute.snapshot.params['id'];
    this._warehouseService.getWarehouseById({ id: id }).subscribe((res) => {
      this.name.setValue(res.data?.name ?? '');
      this.address.setValue(res.data?.address ?? '');
      this._loadingService.hide();
    });
  }

  onUpdate() {
    this._loadingService.show();
    const name = this.name.value ?? '';
    const address = this.address.value ?? '';
    const id = this._activatedRoute.snapshot.params['id'];
    this._warehouseService
      .updateWarehouse({
        id: id,
        name: name,
        address: address,
      })
      .subscribe((res) => {
        this._loadingService.hide();
        if (
          res.isNormal &&
          res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS
        ) {
          this._router.navigate([UrlConstEnum.WAREHOUSE_INDEX]);
        }
      });
  }
}
