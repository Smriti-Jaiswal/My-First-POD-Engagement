import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { DepositeBaseComponent } from './deposite-base.component';
import { CreateDepositeComponent } from './create/create-deposite.component';

const routes: Routes = [{
  path: '',
  component: DepositeBaseComponent,
  children: [
    {
      path: 'create',
      component: CreateDepositeComponent
    }
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class DepositeRoutingModule {
}
