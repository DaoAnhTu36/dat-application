import { Component, ElementRef, Renderer2 } from '@angular/core';
import { WarehouseService } from '../../services/warehouse-service.service';
import { FormBuilder } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CommonServiceService } from '../../services/common-service.service';
import { Router } from '@angular/router';
import { NgFor } from '@angular/common';
import { CustomCurrencyPipe } from '../../commons/pipes/custom-currency.pipe';

@Component({
  selector: 'app-vending-machine',
  standalone: true,
  imports: [NgFor, CustomCurrencyPipe],
  templateUrl: './vending-machine.component.html',
  styleUrl: './vending-machine.component.scss',
})
export class VendingMachineComponent {
  data_buy: any[] = [
    {
      productCode: '1234',
      productName: 'Thuốc là thăng long ',
      unitPrice: 10000,
      unitName: 'Túp',
      quantity: 2,
    },
    {
      productCode: '1234',
      productName: 'Thuốc là thăng long',
      unitPrice: 10000,
      unitName: 'Túp',
      quantity: 2,
    },
    {
      productCode: '1234',
      productName: 'Thuốc là thăng long',
      unitPrice: 10000,
      unitName: 'Túp',
      quantity: 2,
    },
    {
      productCode: '1234',
      productName: 'Thuốc là thăng long',
      unitPrice: 10000,
      unitName: 'Túp',
      quantity: 2,
    },
  ];
  totalPrice: number = 0;
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
      productCode: '1234',
      productName: 'Thuốc là thăng long',
      unitPrice: 10000,
      unitName: 'Túp',
      quantity: 2,
    });
  }
}
