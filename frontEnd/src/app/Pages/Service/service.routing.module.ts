import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import {  ServiceBaseComponent } from './service-base.component';
import {  ServiceIndexComponent } from './index/service-index.component';
import { CreateServiceComponent } from './create/create-service.component';

const routes: Routes = [{
  path: '',
  component: ServiceBaseComponent,
  children: [
    {
      path: 'index',
      component: ServiceIndexComponent
    },
    {
      path: 'create',
      component: CreateServiceComponent
    }
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class ServiceRoutingModule {
}
