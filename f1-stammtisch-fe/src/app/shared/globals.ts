import {inject} from '@angular/core';
import {MessageService} from 'primeng/api';

export const BASE_URL = 'http://localhost:5000/api';

export class Globals {
  private messageService = inject(MessageService);

  showMessage(message: string, severity:'success' | 'warn' | 'error') {
    this.messageService.add({
      severity: severity,
      detail: message,
      key: 'tl',
      life: 3000
    });
  }
}
