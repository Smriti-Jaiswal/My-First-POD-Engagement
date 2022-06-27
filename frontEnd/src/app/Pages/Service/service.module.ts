import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/_Shared/shared.module';
import { CreateServiceComponent } from './create/create-service.component';
import { ServiceIndexComponent } from './index/service-index.component';
import { ServiceBaseComponent } from './service-base.component';
import { ServiceRoutingModule } from './service.routing.module';


@NgModule({
  imports: [
    SharedModule,
    ServiceRoutingModule,
  ],
  declarations: [
    ServiceIndexComponent,
    CreateServiceComponent,
    ServiceBaseComponent
  ],
})
export class ServiceModule {
}
