import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'auth-base',
  template: `

      <router-outlet></router-outlet>

  `,
  styles: []
})
export class AuthBaseComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
