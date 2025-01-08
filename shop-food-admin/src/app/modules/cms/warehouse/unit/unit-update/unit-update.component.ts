import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { StatusCodeApiResponse } from '../../../../../commons/const/ConstStatusCode';
import { LoadingService } from '../../../../../commons/loading/loading.service';
import { WarehouseService } from '../../../../../services/warehouse-service.service';
import { ToastrService } from 'ngx-toastr';
import { UrlConstEnum } from '../../../../../menu/config-url';

@Component({
  selector: 'app-unit-update',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './unit-update.component.html',
  styleUrl: './unit-update.component.scss',
})
export class UnitUpdateComponent {
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly _activatedRoute: ActivatedRoute,
    private readonly _loadingService: LoadingService,
    private readonly _router: Router,
    private readonly _toastService: ToastrService
  ) {}
  name = new FormControl('');

  ngOnInit() {
    this.getDetailById();
  }

  getDetailById() {
    this._loadingService.show();
    const id = this._activatedRoute.snapshot.params['id'];
    this._warehouseService.unitDetail({ id: id }).subscribe((res) => {
      this._loadingService.hide();
      if (
        res.isNormal &&
        res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS
      ) {
        this.name.setValue(res.data?.name ?? '');
      } else {
      }
    });
  }

  update() {
    this._loadingService.show();
    const name = this.name.value ?? '';
    const id = this._activatedRoute.snapshot.params['id'];
    this._warehouseService
      .unitUpdate({
        id: id,
        name: name,
      })
      .subscribe((res) => {
        this._loadingService.hide();
        if (
          res.isNormal &&
          res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS
        ) {
          this._router.navigate([UrlConstEnum.UNIT_INDEX]);
        } else {
          this._toastService.error('Thất bại');
        }
      });
  }
}
