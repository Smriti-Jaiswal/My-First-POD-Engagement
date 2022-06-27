import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-service-base',
  template: `<router-outlet></router-outlet>`,
})
export class ServiceBaseComponent implements OnInit {
  ngOnInit(): void {
  }
}