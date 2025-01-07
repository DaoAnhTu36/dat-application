import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharingService {

  constructor() { }
  private dataSource = new Subject<string>();
  data$ = this.dataSource.asObservable();

  sendData(data: string) {
    this.dataSource.next(data);
  }
}
