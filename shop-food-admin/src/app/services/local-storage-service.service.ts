import * as CryptoJS from 'crypto-js';
import { isPlatformBrowser } from '@angular/common';
import { Inject, Injectable, PLATFORM_ID } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageServiceService {
  secret_key_crypto = "DaoAnhTu020996@@#";
  constructor(@Inject(PLATFORM_ID) private platformId: Object) { }

  saveData(key: string, value: string) {
    if (isPlatformBrowser(this.platformId)) {
      value = this.encrypt(value);
      localStorage.setItem(key, value);
    }
  }

  getData(key: string) {
    if (isPlatformBrowser(this.platformId)) {
      const value = localStorage.getItem(key);
      if (value) {
        return this.decrypt(value);
      }
    }
    return null;
  }

  removeData(key: string) {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem(key);
    }
  }

  clearData() {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.clear();
    }
  }

  public encrypt(instance: string): string {
    return CryptoJS.AES.encrypt(instance, this.secret_key_crypto).toString();
  }

  public decrypt(instance: string) {
    return CryptoJS.AES.decrypt(instance, this.secret_key_crypto).toString(CryptoJS.enc.Utf8);
  }

}
