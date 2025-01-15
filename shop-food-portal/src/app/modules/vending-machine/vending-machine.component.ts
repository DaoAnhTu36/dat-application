import { Component, ElementRef, Renderer2 } from '@angular/core';
import { WarehouseService } from '../../services/warehouse-service.service';
import { FormBuilder, FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CommonServiceService } from '../../services/common-service.service';
import { Router } from '@angular/router';
import { CommonModule, NgFor } from '@angular/common';
import { CustomCurrencyPipe } from '../../commons/pipes/custom-currency.pipe';

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
  quantity: number | undefined;
  unitName: string | undefined;
  price: number | undefined;
  goodsName: string | undefined;
  totalPrice: number = 0;
  totalBill: number = 0;
  totalUnitPrice: number = 0;

  constructor(
    private readonly _warehouseService: WarehouseService,
    private readonly _router: Router,
    private readonly _toastService: ToastrService,
    private readonly _renderer: Renderer2,
    private readonly _el: ElementRef,
    private readonly fb: FormBuilder,
    private readonly _commonService: CommonServiceService
  ) {}

  ngOnInit() {}

  onAdd() {
    this.data_buy.push({
      goodsCode: this.goodsCode,
      goodsName: this.goodsName,
      price: this.price,
      unitName: this.unitName,
      quantity: this.quantity,
      totalUnitPrice: (this.quantity ?? 0) * (this.price ?? 0),
    });
    this.onCalTotalBill();
    this.goodsCode = '';
    this.goodsName = '';
    this.price = undefined;
    this.unitName = '';
    this.quantity = undefined;
    this.totalUnitPrice = 0;
  }

  onSearchGoods(event: Event) {
    const textSearch = (event.target as HTMLSelectElement).value;
    this._warehouseService
      .goodsretailSearch({
        textSearch: textSearch,
      })
      .subscribe((res) => {
        this.goodsName = res.data?.goodsName ?? '';
        this.price = res.data?.price ?? undefined;
        this.unitName = res.data?.unitName ?? '';
        this.quantity = 1;
        this.totalPrice += this.quantity * (this.price ?? 0);
      });
  }

  onCalTotalPrice() {
    const quantity = this.quantity ?? 0;
    this.totalUnitPrice = quantity * (this.price ?? 0);
    this.onCalTotalBill();
  }

  onCalTotalBill() {
    this.totalBill = 0;
    this.data_buy.map((x) => {
      return (this.totalBill += x.totalUnitPrice);
    });
  }
}
