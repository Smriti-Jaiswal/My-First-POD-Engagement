import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { AuthBaseComponent } from './auth.component';
import { AuthRoutingModule } from './auth.router.module';
import { LoginComponent } from './Login/login.component';


@NgModule({
   declarations: [AuthBaseComponent, LoginComponent ],
   imports: [
       CommonModule,
       AuthRoutingModule,FormsModule]
})
export class AuthModule { }
