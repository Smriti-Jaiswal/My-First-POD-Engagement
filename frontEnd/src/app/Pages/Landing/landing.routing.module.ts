import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { LandingBaseComponent } from './landing-base.component';
import { LandingIndexComponent } from './index/landing-index.component';

const routes: Routes = [{
  path: '',
  component: LandingBaseComponent,
  children: [
    {
      path: 'index',
      component: LandingIndexComponent
    }
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class LandingRoutingModule {
}
