import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { SidebarModule } from '../../shared/sidebar/sidebar-module';
import { NavbarModule } from '../../shared/navbar/navbar-module';



@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
    SidebarModule,
    NavbarModule
  ]
})
export class DashboardModule { }
