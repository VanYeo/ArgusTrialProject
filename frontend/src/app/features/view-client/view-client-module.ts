import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ViewClient } from './view-client';
import { RouterModule } from '@angular/router';
import { M } from "../../../../node_modules/@angular/material/form-field.d-C6p5uYjG";
import { MatInputModule } from "@angular/material/input";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    ViewClient
  ],
  imports: [
    CommonModule,
    RouterModule,
    M,
    MatInputModule,
    ReactiveFormsModule
],
  exports: [ViewClient]
})
export class ViewClientModule { }
