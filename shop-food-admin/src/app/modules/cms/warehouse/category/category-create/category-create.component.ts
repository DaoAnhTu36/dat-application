import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { StatusCodeApiResponse } from '../../../../../commons/const/ConstStatusCode';
import { UrlConstEnum } from '../../../../../menu/config-url';
import { WarehouseService } from '../../../../../services/warehouse-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-category-create',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './category-create.component.html',
  styleUrl: './category-create.component.scss',
})
export class CategoryCreateComponent {
  name = new FormControl('');
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly _router: Router,
    private readonly _toastService: ToastrService
  ) {}
  create() {
    const nameValue = this.name.value ?? '';
    this._warehouseService
      .categoryCreate({
        name: nameValue,
      })
      .subscribe((res) => {
        if (
          res.isNormal &&
          res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
        ) {
          this._router.navigate([UrlConstEnum.CATEGORY_INDEX]);
        } else {
          this._toastService.error('Thất bại');
        }
      });
  }
}
