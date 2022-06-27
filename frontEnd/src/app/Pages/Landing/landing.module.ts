import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { LandingIndexComponent } from './index/landing-index.component';
import { LandingBaseComponent } from './landing-base.component';
import { LandingRoutingModule } from './landing.routing.module';


@NgModule({
  imports: [
    CommonModule,
    LandingRoutingModule,
  ],
  declarations: [
    LandingBaseComponent,
    LandingIndexComponent
  ],
})
export class LandingModule {
}
