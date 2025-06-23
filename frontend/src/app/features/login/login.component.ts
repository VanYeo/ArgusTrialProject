import { Component, inject } from '@angular/core';
import { LoginService } from './login.service';
import { TmplAstDeferredBlockLoading } from '@angular/compiler';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.css',
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
    this.router.navigate(['/forgot-password'])
  }

  onSubmit() {
    this.assignErrorMessage();
    if (!this.disableSubmit) {
      this.errorMessage = '';
      this.loginService.submitLoginData(this.loginData).subscribe({
        next: (token) => {
          console.log('Token:', token);
          this.errorMessage = '';
          this.router.navigate(['/dashboard']);
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
