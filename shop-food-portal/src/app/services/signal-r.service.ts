import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../environments/environment.development';
import { SharingService } from './sharing.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  hubConnection: signalR.HubConnection = new signalR.HubConnectionBuilder().withUrl(`${environment.API_WAREHOUSE_URL}/realtime-api`).build();
  messages: { message: string }[] = [];

  constructor(private readonly _sharedService: SharingService) {
    this.startConnection();
    this.addReceiveMessageListener();
  }

  startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`${environment.API_WAREHOUSE_URL}/realtime-api`)
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('SignalR Connected'))
      .catch((err) => console.error('SignalR Connection Error: ', err));
  }

  addReceiveMessageListener(): void {
    this.hubConnection.on('SendMessageFromServerToClient', (message: string) => {
      if (this.messages.length > 2) {
        this.messages = [];
      }
      this.messages.push({ message });
      console.log('SignalR Received tutv19: ', JSON.stringify(this.messages));
      this._sharedService.sendData(message);
    });
  }

  sendMessage(message: string): void {
    this.hubConnection
      .invoke('SendMessageToClient', message)
      .catch((err) => console.error('SignalR Send Error: ', err));
  }
}
