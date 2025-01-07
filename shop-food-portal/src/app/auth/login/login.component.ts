import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { AdminSignInDTORequest, AuthService } from '../../services/auth-service.service';
import { CommonServiceService } from '../../services/common-service.service';
import { Router } from '@angular/router';
import { LocalStorageServiceService } from '../../services/local-storage-service.service';
import { json } from 'stream/consumers';
import { LoadingService } from '../../commons/loading/loading.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  email = new FormControl('tranvantu@gmail.com');
  password = new FormControl('123@123');
  /**
   *
   */
  constructor(
    private readonly _authService: AuthService,
    private readonly _commonService: CommonServiceService,
    private readonly _router: Router,
    private readonly _localStorage: LocalStorageServiceService,
    private readonly _loadingService: LoadingService,
  ) {

  }
  signIn() {
    this._loadingService.show();
    let email = this.email.value ?? '';
    let password = this.password.value ?? '';
    const request: AdminSignInDTORequest = {
      email: email,
      password: password
    }
    this._authService.signIn(request).subscribe((response) => {
      this._loadingService.hide();
      this._localStorage.saveData('CustomerInfor', JSON.stringify({
        accessToken: response.data?.accessToken,
        email: response.data?.email,
        expiredDate: response.data?.expiredDate,
        name: response.data?.name,
      }));
      this._router.navigate(['/warehouse']);
    });
  }
}
