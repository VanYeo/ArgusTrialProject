import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientsTable } from './clients-table';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ClientsTable
  ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [
    ClientsTable
  ]
})
export class ClientsTableModule { }
