import { NgModule } from '@angular/core';
import { SharedModule } from 'src/app/_Shared/shared.module';
import { CreateUserComponent } from './create/create-user.component';
import { UserIndexComponent } from './index/user-index.component';
import { UserRoutingModule } from './users.routing.module';
import { UserBaseComponent } from './user-base.component';


@NgModule({
  imports: [
    SharedModule,
    UserRoutingModule,
  ],
  declarations: [
    UserIndexComponent,
    CreateUserComponent,
    UserBaseComponent
  ],
})
export class UserModule {
}
