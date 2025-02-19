import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import {
  API_WAREHOUSE_URL,
  WarehouseService,
} from './services/warehouse-service.service';
import { API_AUTH_URL, AuthService } from './services/auth-service.service';
import { environment } from '../environments/environment';
import { LoadingComponent } from './commons/loading/loading.component';
import { LoadingService } from './commons/loading/loading.service';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoadingInterceptor } from './commons/loading/loading.interceptor';
import { SignalRService } from './services/signal-r.service';
import { SharingService } from './services/sharing.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, LoadingComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  providers: [
    WarehouseService,
    AuthService,
    { provide: API_AUTH_URL, useValue: environment.API_AUTH_URL },
    { provide: API_WAREHOUSE_URL, useValue: environment.API_WAREHOUSE_URL },
    LoadingService,
    // , SignalRService
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingInterceptor,
      multi: true,
    },
  ],
})
export class AppComponent {
  loading$: any;
  constructor(
    private loadingService: LoadingService,
    // private readonly _signalRService: SignalRService,
    // private readonly _sharingService: SharingService,
    private readonly _toastrService: ToastrService,
    private readonly _router: ActivatedRoute
  ) {
    this.loading$ = this.loadingService.loading$;
    // this._sharingService.data$.subscribe(data => {
    //   const dataJson = JSON.parse(data);
    //   this._toastrService.info(dataJson["Body"], dataJson["Title"]);
    // });
  }
  title = '';

  onNextPage() {
    console.log('next page');
  }

  onPreviousPage() {
    console.log('previous page');
    window.history.back();
  }
}
