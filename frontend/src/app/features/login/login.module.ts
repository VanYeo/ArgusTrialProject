import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';         
import { RouterModule } from '@angular/router';      
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login.component';

@NgModule({
  declarations: [
    LoginComponent           
  ],
  imports: [
    CommonModule,          
    FormsModule,         
    ReactiveFormsModule,    
    RouterModule            
  ],
  exports: [
    LoginComponent         
  ]
})
export class LoginModule {}
