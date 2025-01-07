import { Pipe, PipeTransform } from '@angular/core';
import { CommonServiceService } from '../../services/common-service.service';

@Pipe({
  name: 'customCurrencyPipe',
  standalone: true,
})
export class CustomCurrencyPipe implements PipeTransform {
  constructor(private readonly _commonService: CommonServiceService) {}
  transform(value: string | number | undefined): any {
    return this._commonService.formatCurrency(value);
  }
}
