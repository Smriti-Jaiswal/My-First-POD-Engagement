import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/_Shared/shared.module';
import { CreateTransactionComponent } from './create/create-transaction.component';
import { TransactionIndexComponent } from './index/transaction-index.component';
import { TransactionRoutingModule } from './transaction.routing.module';
import { TransactionBaseComponent } from './trancation-base.component';


@NgModule({
  imports: [
    SharedModule,
    TransactionRoutingModule,
  ],
  declarations: [
    TransactionIndexComponent,
    CreateTransactionComponent,
    TransactionBaseComponent
  ],
})
export class TransactionModule {
}
