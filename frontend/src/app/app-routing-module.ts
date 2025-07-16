import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ForgotPasswordComponent } from './features/forgot-password/forgot-password.component';
import { LoginComponent } from './features/login/login.component';
import { DashboardComponent } from './features/dashboard/dashboard.component';
import { authGuard } from './app-guard';
import { Assets } from './features/assets/assets';
import { ViewClient } from './features/view-client/view-client';
import { AddClientComponent } from './features/add-client/add-client';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  {
    path: 'clients',
    canActivate: [authGuard],
    component: DashboardComponent,
    data: { breadcrumb: 'Home' },
  },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'assets', canActivate: [authGuard], component: Assets },
  {
    path: 'clients/add',
    canActivateChild: [authGuard],
    component: AddClientComponent,
  },
  { path: 'clients/:id', canActivate: [authGuard], component: ViewClient },
  {
    path: 'clients/edit/:id',
    canActivateChild: [authGuard],
    component: AddClientComponent,
    runGuardsAndResolvers: 'always',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
