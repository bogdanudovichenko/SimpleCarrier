import {RouterModule, Routes} from "@angular/router";
import {LoginComponent} from "./login/login.component";

export const AuthRoutes: Routes = [
  {
    path: '',
    children: [
      { path: 'login', component: LoginComponent },
    ]
  }
];

export const AuthRoutingModule = RouterModule.forChild(AuthRoutes);
