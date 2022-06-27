import { Component, OnInit } from '@angular/core';
import { UtilityService } from 'src/app/Service/Hospital/all.service';
import jwt_decode from "jwt-decode";


@Component({
  selector: 'app-landing-index',
  templateUrl: './landing-index.component.html',
  styleUrls: ['./landing-index.component.scss']
})
export class LandingIndexComponent implements OnInit {
  currentUserDetails :any;

  constructor(
    private utilService: UtilityService
  ) {}
  resp:any;
  ngOnInit(): void {
    this.currentUserDetails = jwt_decode(localStorage.getItem('auth') ?? "");
    this.utilService.GetBalaceDetails()
    .subscribe(resp => {
      console.log(resp);
      this.resp = resp;
    })
  }
}