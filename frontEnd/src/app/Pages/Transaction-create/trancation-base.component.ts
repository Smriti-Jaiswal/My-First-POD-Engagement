import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-transaction-base',
  template: `<router-outlet></router-outlet>`,
})
export class TransactionBaseComponent implements OnInit {
  ngOnInit(): void {
  }
}