import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthBaseComponent } from './auth.component';
import { LoginComponent } from './Login/login.component';

const routes: Routes = [{
  path: '',
  component: AuthBaseComponent,
  children: [
    {
      path: 'login',
      component: LoginComponent,
    },
    {
      path: '',
      redirectTo: 'login',
      pathMatch: 'full',
    },
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
