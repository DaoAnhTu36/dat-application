import { Component, ElementRef, Renderer2 } from '@angular/core';
import {
  GoodsRetailWhListModelRes,
  TranRetailDTO,
  WarehouseService,
} from '../../services/warehouse-service.service';
import { FormBuilder, FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CommonServiceService } from '../../services/common-service.service';
import { Router } from '@angular/router';
import { CommonModule, NgFor } from '@angular/common';
import { CustomCurrencyPipe } from '../../commons/pipes/custom-currency.pipe';
import {
  PageingReq,
  StatusCodeApiResponse,
} from '../../commons/const/ConstStatusCode';

@Component({
  selector: 'app-vending-machine',
  standalone: true,
  imports: [NgFor, CustomCurrencyPipe, FormsModule],
  templateUrl: './vending-machine.component.html',
  styleUrl: './vending-machine.component.scss',
})
export class VendingMachineComponent {
  data_buy: any[] = [];
  goodsCode: string | undefined;
  goodsId: string | undefined;
  quantity: number | undefined;
  unitName: string | undefined;
  unitId: string | undefined;
  transDetailId: string | undefined;
  price: number | undefined;
  goodsName: string | undefined;
  totalPrice: number = 0;
  totalBill: number = 0;
  listGoodsRetail: GoodsRetailWhListModelRes | undefined;

  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly _router: Router,
    private readonly _toastService: ToastrService,
    private readonly _renderer: Renderer2,
    private readonly _el: ElementRef,
    private readonly fb: FormBuilder,
    private readonly _commonService: CommonServiceService
  ) {}

  ngOnInit() {
    this.getGoodsRetailList();
  }

  onAdd() {
    this.data_buy.push({
      goodsCode: this.goodsCode,
      goodsName: this.goodsName,
      price: this.price,
      unitName: this.unitName,
      quantity: this.quantity,
      goodsId: this.goodsId,
      unitId: this.unitId,
      transDetailId: this.transDetailId,
    });
    this.goodsCode = '';
    this.goodsId = '';
    this.goodsName = '';
    this.price = undefined;
    this.unitName = '';
    this.quantity = undefined;
  }

  getGoodsRetailList() {
    this._warehouseService.goodsretailListForMachine().subscribe((res) => {
      if (
        res.isNormal &&
        res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS
      ) {
        this.listGoodsRetail = res.data;
      }
    });
  }

  onSearchGoods(event: Event) {
    const textSearch = (event.target as HTMLSelectElement).value;
    this._warehouseService
      .goodsretailSearch({
        textSearch: textSearch,
      })
      .subscribe((res) => {
        if (
          res.isNormal &&
          res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS
        ) {
          this.goodsId = res.data?.goodsId ?? '';
          this.unitId = res.data?.unitId ?? '';
          this.transDetailId = res.data?.transDetailId ?? '';
          this.goodsName = res.data?.goodsName ?? '';
          this.price = res.data?.price ?? undefined;
          this.unitName = res.data?.unitName ?? '';
          this.quantity = 1;
          this.totalPrice += this.quantity * (this.price ?? 0);
          this.onAdd();
          this.onCalTotalBill();
        } else {
          this._toastService.error('Không tìm thấy sản phẩm');
        }
      });
  }

  onCalTotalBill(i: number = 0, quantity: number = 0) {
    if (i !== 0 && quantity !== 0) {
      this.data_buy[i].quantity = quantity;
    }
    this.totalBill = 0;
    this.data_buy.map((x) => {
      return (this.totalBill += x.price * x.quantity);
    });
  }

  onPay() {
    let req: TranRetailDTO[] = [];
    this.data_buy.forEach((e) => {
      req.push({
        goodsCode: e.goodsCode,
        goodsId: e.goodsId,
        goodsName: e.goodsName,
        price: e.price,
        quantity: e.quantity,
        transDetailId: e.transDetailId,
        unitId: e.unitId,
      });
    });
    this._warehouseService
      .transactionretailCreate({
        items: req,
      })
      .subscribe((res) => {
        if (
          res.isNormal &&
          res.metaData?.statusCode === StatusCodeApiResponse.SUCCESS
        ) {
          this._toastService.success('Thanh toán thành công');
          this.data_buy = [];
          this.goodsCode = '';
          this.goodsId = '';
          this.goodsName = '';
          this.price = undefined;
          this.unitName = '';
          this.quantity = undefined;
          this.totalBill = 0;
        } else {
          this._toastService.success('Thanh toán thất bại');
        }
      });
  }
}
