import { RouterModule, Routes } from '@angular/router';
import { NgModule } from '@angular/core';
import { PagesComponent } from './pages-base.component';

const routes: Routes = [{
  path: '',
  component: PagesComponent,
  children: [
    {
      path: 'users',
      loadChildren: () => import('./User-create/users.module')
        .then(m => m.UserModule),
    },
    {
      path: 'transaction',
      loadChildren: () => import('./Transaction-create/transaction.module')
        .then(m => m.TransactionModule),
    },
    {
      path: 'deposite',
      loadChildren: () => import('./Deposite/deposite.module')
        .then(m => m.DepositeModule),
    },
    {
      path: 'service',
      loadChildren: () => import('./Service/service.module')
        .then(m => m.ServiceModule),
    },
    {
      path: 'landing',
      loadChildren: () => import('./Landing/landing.module')
        .then(m => m.LandingModule),
    },
    {
      path: '',
      redirectTo: 'pages/landing',
      pathMatch: 'full',
    },
  ],
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PagesRoutingModule {
}
