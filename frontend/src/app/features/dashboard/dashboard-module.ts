import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { SidebarModule } from '../../shared/sidebar/sidebar-module';
import { NavbarModule } from '../../shared/navbar/navbar-module';
import { MatSidenav, MatSidenavContainer, MatSidenavContent, MatSidenavModule } from '@angular/material/sidenav';
import { RouterModule, RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatDividerModule } from '@angular/material/divider';
import { SearchComboboxModule } from '../../shared/search-combobox/search-combobox-module';
import { AddClientModule } from '../add-client/add-client-module';
import { ViewClientModule } from '../view-client/view-client-module';
import { ClientsTableModule } from '../clients-table/clients-table-module';



@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
    SidebarModule,
    NavbarModule,
    MatSidenavModule,
    RouterModule,
    FormsModule,
    MatMenuModule,
    MatButtonModule,
    MatCheckboxModule,
    MatDividerModule,
    SearchComboboxModule,
    AddClientModule,
    RouterModule,
    ClientsTableModule
  ]
})
export class DashboardModule { }
