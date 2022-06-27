import { Component, OnInit, OnDestroy } from '@angular/core';
import { takeUntil } from 'rxjs/operators';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/Service/Auth/auth.service';

class LoginModel
{
  email?: string;
  password?: string;
  token?: string
}

@Component({
  selector: 'login-app',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit,OnDestroy {

  errorMessage: string;

  login:LoginModel = new LoginModel();

  constructor(
    private router:Router,
    private authService: AuthService
  ) {
    this.errorMessage = '';
    localStorage.clear();
    this.authService.currentUserSubject.next(null as any);
  }

  ngOnInit(): void {

  }

  submit(): void {
    this.authService.Login(this.login)
    .subscribe((resp) => {
      console.log(resp);
      localStorage.setItem('auth', JSON.stringify(resp.model.token));
        this.authService.currentUserSubject.next(resp.model.token);
        this.router.navigate(["pages/landing/index"]);
    },(err) => {
      console.log(err)
      this.errorMessage = err.error.message;
    })
  }

  ngOnDestroy():void
  {
  }

}
