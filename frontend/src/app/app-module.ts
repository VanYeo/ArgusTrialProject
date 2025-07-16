import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { LoginModule } from './features/login/login.module';
import { ForgotPasswordModule } from './features/forgot-password/forgot-password-module';
import { SidebarModule } from './shared/sidebar/sidebar-module';
import { NavbarModule } from './shared/navbar/navbar-module';
import { DashboardModule } from './features/dashboard/dashboard-module';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BreakpointObserver } from '@angular/cdk/layout';

@NgModule({
  declarations: [
    App,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    LoginModule,
    ForgotPasswordModule,
    SidebarModule,
    DashboardModule,
    RouterModule,
    BrowserAnimationsModule,
    NavbarModule,
    MatSidenavModule,
  ],
  providers: [
    provideBrowserGlobalErrorListeners()
  ],
  bootstrap: [App]
})
export class AppModule { }
