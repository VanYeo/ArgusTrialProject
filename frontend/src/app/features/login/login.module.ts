import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';         
import { RouterModule } from '@angular/router';      
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login.component';
import { ToastModule } from '../../shared/toast/toast-module';

@NgModule({
  declarations: [
    LoginComponent           
  ],
  imports: [
    CommonModule,          
    FormsModule,         
    ReactiveFormsModule,    
    RouterModule,
    ToastModule            
  ],
  exports: [
    LoginComponent         
  ]
})
export class LoginModule {}
