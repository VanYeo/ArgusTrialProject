import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-toast',
  standalone: false,
  templateUrl: './toast.html',
  styleUrl: './toast.scss'
})
export class Toast {
  @Input() message: string = '';
  @Input() type: 'success' | 'error' | 'info' = 'success';
  show = false;

  showToast(message: string, type: 'success' | 'error' | 'info' = 'success') {
    this.message = message;
    this.type = type;
    this.show = true;

    setTimeout(() => {
      this.show = false;
    }, 3000);
  }
}
