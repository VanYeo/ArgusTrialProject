import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SearchCombobox } from './search-combobox';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    SearchCombobox
  ],
  imports: [
    CommonModule,
    FormsModule
  ],
  exports: [
    SearchCombobox
  ]
})
export class SearchComboboxModule { }
