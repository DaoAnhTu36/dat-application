import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { WarehouseService } from '../../../../../services/warehouse-service.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { StatusCodeApiResponse } from '../../../../../commons/const/ConstStatusCode';
import { UrlConstEnum } from '../../../../../menu/config-url';

@Component({
  selector: 'app-unit-create',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './unit-create.component.html',
  styleUrl: './unit-create.component.scss',
})
export class UnitCreateComponent {
  name = new FormControl('');
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly _router: Router,
    private readonly _toastService: ToastrService
  ) {}
  create() {
    const nameValue = this.name.value ?? '';
    this._warehouseService
      .createUnit({
        name: nameValue,
      })
      .subscribe((res) => {
        if (
          res.isNormal &&
          res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
        ) {
          this._router.navigate([UrlConstEnum.UNIT_INDEX]);
        } else {
          this._toastService.error('Lưu thất bại');
        }
      });
  }
}
