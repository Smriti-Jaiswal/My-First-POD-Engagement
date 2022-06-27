import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import {  UserBaseComponent } from './user-base.component';
import {  UserIndexComponent } from './index/user-index.component';
import { CreateUserComponent } from './create/create-user.component';

const routes: Routes = [{
  path: '',
  component: UserBaseComponent,
  children: [
    {
      path: 'index',
      component: UserIndexComponent
    },
    {
      path: 'create',
      component: CreateUserComponent
    },
    {
      path: 'edit/:id',
      component: CreateUserComponent
    }
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class UserRoutingModule {
}
