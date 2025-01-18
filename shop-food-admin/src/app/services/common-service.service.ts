import { Injectable } from '@angular/core';
import { CustomerInformationDTO } from '../commons/ngrx/models';

@Injectable({
  providedIn: 'root',
})
export class CommonServiceService {
  _global_customer_infor: CustomerInformationDTO = {
    name: '',
    email: '',
    accessToken: '',
    expiredDate: new Date(),
  };

  constructor() {}

  getGlobalCustomerInfor(): CustomerInformationDTO {
    return this._global_customer_infor;
  }

  setGlobalCustomerInfor(customerInfor: CustomerInformationDTO) {
    this._global_customer_infor = customerInfor;
  }

  formatNumber(value: any): string {
    if (isNaN(value) || value === null || value === undefined || value === '') {
      return '0';
    }
    const formatter = new Intl.NumberFormat('vi-VN');
    return formatter.format(value);
  }

  // formatCurrency(value: number): string {
  //   return value.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
  // }

  // formatCurrencyVND(value: number): string {
  //   return value.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,') + ' VND';
  // }

  parseStringToInt(value: any): number {
    if (isNaN(value) || value === null || value === undefined) {
      return 0;
    }
    return parseInt(value.replace(/,/g, ''), 10);
  }

  formatCurrency(value: any): string {
    const formatter = new Intl.NumberFormat('vi-VN', {
      style: 'currency',
      currency: 'VND',
    });
    if (isNaN(value) || value === null || value === undefined) {
      return formatter.format(0);
    }

    return formatter.format(value);
  }

  revertFormatCurrency(currencyString: string): number {
    const normalizedString = currencyString
      .toString()
      .replace(new RegExp(`\\.`, 'g'), '') // Remove grouping separators
      .replace(new RegExp(`\\,`), '.'); // Replace decimal separator with '.'
    return parseFloat(normalizedString.replace(/[^0-9.-]/g, ''));
    // const numericString = currencyString.replace(/[^0-9.,-]+/g, ''); // Retain numbers and separators
    // const parts = new Intl.NumberFormat('vi-VN').formatToParts(1234.56);
    // const decimalSeparator =
    //   parts.find((part) => part.type === 'decimal')?.value || '.';
    // const groupingSeparator =
    //   parts.find((part) => part.type === 'group')?.value || ',';

    // const normalizedString = numericString
    //   .replace(new RegExp(`\\${groupingSeparator}`, 'g'), '')
    //   .replace(new RegExp(`\\${decimalSeparator}`), '.');

    // return parseFloat(normalizedString);
  }
}
