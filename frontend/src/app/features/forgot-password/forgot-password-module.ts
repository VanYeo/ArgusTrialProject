import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ForgotPasswordComponent } from './forgot-password.component';
import { SidebarModule } from "../../shared/sidebar/sidebar-module";



@NgModule({
  declarations: [
    ForgotPasswordComponent
  ],
  imports: [
    CommonModule,
    SidebarModule
]
})
export class ForgotPasswordModule { }
