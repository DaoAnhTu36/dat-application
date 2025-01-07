import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { WarehouseService } from '../../../../../services/warehouse-service.service';
import { Router } from '@angular/router';
import { StatusCodeApiResponse } from '../../../../../commons/const/ConstStatusCode';
import { ToastrService } from 'ngx-toastr';
import { UrlConstEnum } from '../../../../../menu/config-url';

@Component({
  selector: 'app-supplier-create',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './supplier-create.component.html',
  styleUrl: './supplier-create.component.scss',
})
export class SupplierCreateComponent {
  name = new FormControl('Nhà cung cấp');
  address = new FormControl(
    'Số nhà 5A ngõ 221 Yên Xá Tân Triều Thanh Trì Hà Nội'
  );
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly _router: Router,
    private readonly _toastService: ToastrService
  ) {}
  onCreate() {
    const name = this.name.value ?? '';
    const address = this.address.value ?? '';
    this._warehouseService
      .createSupplier({
        name: name,
        address: address,
      })
      .subscribe((res) => {
        if (
          res.isNormal &&
          res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS
        ) {
          this._router.navigate([UrlConstEnum.SUPPLIER_INDEX]);
        } else {
          this._toastService.error('Lưu thất bại');
        }
      });
  }
}
