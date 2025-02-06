import { Component } from '@angular/core';
import { ReactiveFormsModule, FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import {
  PageingReq,
  StatusCodeApiResponse,
} from '../../../../../commons/const/ConstStatusCode';
import {
  CategoryWhListModelRes,
  CategoryWhModel,
  FileParameter,
  GoodsWhCreateModelReq,
  SupplierWhListModelRes,
  UnitWhListModelRes,
  UploadFileResponseDTOApiResponse,
  WarehouseService,
} from '../../../../../services/warehouse-service.service';
import { Router } from '@angular/router';
import { NgFor } from '@angular/common';
import { UrlConstEnum } from '../../../../../menu/config-url';
import { FileUploadService } from '../../../../../services/file-upload.service';

@Component({
  selector: 'app-product-create',
  standalone: true,
  imports: [ReactiveFormsModule, NgFor],
  templateUrl: './product-create.component.html',
  styleUrl: './product-create.component.scss',
})
export class ProductCreateComponent {
  name = new FormControl('');
  description = new FormControl('');
  goodsCode = new FormControl('');
  categoryId = new FormControl('');
  price = new FormControl();
  files = new FormControl();
  categories: CategoryWhModel[] = [];
  selectedFile!: FileList;
  images: string = '';
  lstLinkFileUploaded: string[] = [];
  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly _router: Router,
    private readonly _toastService: ToastrService,
    private readonly _fileUploadService: FileUploadService
  ) {}

  ngOnInit() {
    this.listCategory();
    let currentDatetime = new Date().getTime();
    this.goodsCode.setValue(`H${currentDatetime.toString()}`);
  }

  onSelectFile(event: Event) {
    const target = event.target as HTMLInputElement;
    if (target.files) {
      this.selectedFile = target.files;
      this._fileUploadService.upload(
        this.selectedFile,
        (response) => {
          if (response.data) {
            this._toastService.success('Upload file thành công');
            response.data?.fileIds?.forEach((element: any) => {
              this.images += element.fileName + '{|}';
              this.lstLinkFileUploaded.push(element.path);
            });
          }
        },
        (error) => {
          console.log(error);
        }
      );
    }
  }

  onUploadFile() {}

  onSave() {
    this.processAPI();
  }

  processAPI() {
    var obj: GoodsWhCreateModelReq = {
      name: this.name.value ?? '',
      description: this.description.value ?? '',
      goodsCode: this.goodsCode.value ?? '',
      categoryId: this.categoryId.value ?? '',
      price: this.price.value ?? 0,
      image: this.images,
    };
    this._warehouseService.goodsCreate(obj).subscribe((res) => {
      if (
        res.isNormal &&
        res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
      ) {
        // this._router.navigate([UrlConstEnum.GOODS_INDEX]);
        this.name.setValue('');
        this.description.setValue('');
        this.goodsCode.setValue('');
        this.categoryId.setValue('');
        this.price.setValue('');
        this.images = '';
      } else {
        this._toastService.error('Thất bại');
      }
    });
  }

  listCategory() {
    this._warehouseService
      .categoryList({
        pageNumber: PageingReq.PAGE_NUMBER,
        pageSize: PageingReq.PAGE_SIZE_SEARCH,
      })
      .subscribe((res) => {
        if (
          res.isNormal &&
          res.metaData?.statusCode == StatusCodeApiResponse.SUCCESS
        ) {
          this.categories = res.data?.list ?? [];
        }
      });
  }
}
