import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-landing-base',
  template: `<router-outlet></router-outlet>`,
})
export class LandingBaseComponent implements OnInit {
  ngOnInit(): void {
  }
}