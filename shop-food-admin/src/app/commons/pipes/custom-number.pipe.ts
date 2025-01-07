import { Pipe, PipeTransform } from '@angular/core';
import { CommonServiceService } from '../../services/common-service.service';

@Pipe({
  name: 'customNumberPipe',
  standalone: true,
})
export class CustomNumberPipe implements PipeTransform {
  constructor(private readonly _commonService: CommonServiceService) {}
  transform(value: string | number | undefined): any {
    return this._commonService.formatNumber(value);
  }
}
