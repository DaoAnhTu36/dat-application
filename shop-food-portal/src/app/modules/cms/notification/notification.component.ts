import { Component } from '@angular/core';
import { SignalRService } from '../../../services/signal-r.service';
import { FormsModule } from '@angular/forms';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [FormsModule, NgFor],
  templateUrl: './notification.component.html',
  styleUrl: './notification.component.scss'
})
export class NotificationComponent {
  message: string = '';

  constructor(public signalRService: SignalRService) { }

  sendMessage(): void {
    this.signalRService.sendMessage(this.message);
  }
}
