import { Pipe, PipeTransform } from '@angular/core';
import { CommonServiceService } from '../../services/common-service.service';

@Pipe({
  name: 'customPipe',
  standalone: true,
})
export class CustomPipe implements PipeTransform {
  constructor(private readonly _commonService: CommonServiceService) {}
  transform(value: string): any {
    return null;
  }
}
