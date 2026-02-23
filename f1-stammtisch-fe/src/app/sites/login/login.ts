import {Component, inject, signal} from '@angular/core';
import {ButtonModule} from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import {FormsModule} from '@angular/forms';
import {AuthService} from '../../shared/services/auth-service';
import {Globals} from '../../shared/globals';


@Component({
  selector: 'app-login',
  imports: [
    FormsModule,
    ButtonModule,
    RippleModule,
    InputTextModule,
    PasswordModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
  authService = inject(AuthService);
  globals = inject(Globals);

  email = signal<string>('');
  password = signal<string>('');

  isAnimating = false;
  isDone = false;

  triggerReveal() {
    this.authService.login(this.email(), this.password()).subscribe({
      next: (response) => {
        const token = response.token
        localStorage.setItem('token', token);

        if (this.isAnimating) return;
        this.isAnimating = true;
        setTimeout(() => {
          this.isDone = true;
        }, 1900);
      },
      error: (err) => {
        this.globals.showMessage('Fehler beim Login', 'error');
      }
    });
  }
}
