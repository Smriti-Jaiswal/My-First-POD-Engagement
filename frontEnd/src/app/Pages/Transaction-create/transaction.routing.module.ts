import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import {  TransactionBaseComponent } from './trancation-base.component';
import {  TransactionIndexComponent } from './index/transaction-index.component';
import { CreateTransactionComponent } from './create/create-transaction.component';

const routes: Routes = [{
  path: '',
  component: TransactionBaseComponent,
  children: [
    {
      path: 'index',
      component: TransactionIndexComponent
    },
    {
      path: 'create',
      component: CreateTransactionComponent
    }
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class TransactionRoutingModule {
}
