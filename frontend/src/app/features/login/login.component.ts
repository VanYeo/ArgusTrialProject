import { Component, inject, ViewChild } from '@angular/core';
import { LoginService } from './login.service';
import { TmplAstDeferredBlockLoading } from '@angular/compiler';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class LoginComponent {
  private router = inject(Router);
  private loginService = inject(LoginService);

  loginData = {
    email: '',
    password: '',
  };

  showPassword: boolean = false;
  errorMessage: string = '';
  disableSubmit: boolean = false;

  togglePassword() {
    this.showPassword = !this.showPassword;
  }

  goToForgotPassword() {
    this.router.navigate(['/forgot-password']);
  }

  @ViewChild('toast') toastComponent!: any;

  onSubmit() {
    this.assignErrorMessage();
    if (!this.disableSubmit) {
      this.errorMessage = '';
      this.loginService.submitLoginData(this.loginData).subscribe({
        next: (res) => {
          localStorage.setItem('authToken', res.jwtToken);
          localStorage.setItem('email', res.email);
          this.toastComponent.showToast(
            'Successful Login',
            'success'
          );
          setTimeout(() => {
            this.router.navigate(['/clients']);
          }, 1000);
          console.log(
            localStorage.getItem('email'),
            localStorage.getItem('authToken')
          );
        },
        error: (err) => {
          console.error(err);
          this.errorMessage = err.error || 'Login failed';
        },
      });
    }
  }

  assignErrorMessage() {
    if (this.loginData.email == '' || this.loginData.password == '') {
      this.errorMessage = 'Email and password fields are required';
      this.disableSubmit = true;
    } else {
      this.disableSubmit = false;
    }
  }
}
