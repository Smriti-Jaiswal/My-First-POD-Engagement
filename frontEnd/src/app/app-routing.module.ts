import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './_Guards/auth.guard';

const routes: Routes = [
  {
    path: 'auth',
    loadChildren: () => import('./Auth/auth.module')
      .then(m => m.AuthModule),
  },
  {
    canActivate: [AuthGuard],
    path: 'pages',
    loadChildren: () => import('./Pages/pages.module')
      .then(m => m.PagesModule),
  },
  { path: '', redirectTo: 'pages/landing', pathMatch: 'full' },
  { path: '**', redirectTo: 'pages/landing' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
