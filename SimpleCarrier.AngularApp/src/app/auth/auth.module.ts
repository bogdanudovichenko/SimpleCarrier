import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {LoginComponent} from './login/login.component';
import {AuthRoutingModule} from './auth-routing.module';
import {AuthService} from './services/auth.service';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AuthRoutingModule
  ],
  declarations: [LoginComponent],
  providers: [AuthService]
})
export class AuthModule {
}
