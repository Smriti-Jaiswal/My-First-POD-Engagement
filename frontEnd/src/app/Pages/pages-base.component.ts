import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../Service/Auth/auth.service';


@Component({
  selector: 'pages-base',
  templateUrl : './pages-base.component.html'
})
export class PagesComponent {
  constructor(
    private authService: AuthService,
    private router: Router,
  ) {

  }
  doLogout(): void {
    localStorage.clear();
    this.authService.currentUserSubject.next(null as any);
    this.router.navigate(["auth/login"]);
  }
}
