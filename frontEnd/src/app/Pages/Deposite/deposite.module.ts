import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/_Shared/shared.module';
import { CreateDepositeComponent } from './create/create-deposite.component';
import { DepositeBaseComponent } from './deposite-base.component';
import { DepositeRoutingModule } from './deposite.routing.module';


@NgModule({
  imports: [
    SharedModule,
    DepositeRoutingModule,
  ],
  declarations: [
    CreateDepositeComponent,
    DepositeBaseComponent
  ],
})
export class DepositeModule {
}
