import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-user-base',
  template: `<router-outlet></router-outlet>`,
})
export class UserBaseComponent implements OnInit {
  ngOnInit(): void {
  }
}