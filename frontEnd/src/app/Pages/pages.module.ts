import { NgModule } from '@angular/core';
import { PagesComponent } from './pages-base.component';
import { PagesRoutingModule } from './pages.routing.module';


@NgModule({
  imports: [
    PagesRoutingModule,
  ],
  declarations: [
    PagesComponent
  ],
})
export class PagesModule {
}
