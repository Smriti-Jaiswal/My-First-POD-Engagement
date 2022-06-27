import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { UtilityService } from 'src/app/Service/Hospital/all.service';

class ReqModel {
  constructor() {
    this.userId = ""
    this.amount = 0;
  }
  id?: number;
  amount: number;
  userId?: string;
}

@Component({
  selector: 'app-deposite-create',
  templateUrl: './create-deposite.component.html',
  styleUrls: ['./create-deposite.component.scss']
})
export class CreateDepositeComponent implements OnInit {
  isEdit: boolean = false;
  userDetails:any[] = [];
  balance:any;
  transactionReqModel: ReqModel = new ReqModel();

  constructor(
    private untiService: UtilityService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    public toaster: MatSnackBar,
  ) { }


  ngOnInit(): void {
    this.prepareDropDown();
  }


  prepareDropDown() {
    this.untiService.UserGetAllV1()
    .subscribe((resp) => {
      if(resp?.model?.length) {
        this.userDetails = resp.model;
      }
    })
  }

  create(): void {
    this.untiService.Deposite(this.transactionReqModel)
      .subscribe((resp) => {
        this.toaster.open("Deposite Successfully", '', {
          duration: 5000,
          verticalPosition: 'top',
          panelClass: ['green-snackbar'],
        });
        this.router.navigate(["/pages/landing/index"])
      }, (err) => {
        this.toaster.open(err.error.message, '', {
          duration: 5000,
          verticalPosition: 'top',
          panelClass: ['red-snackbar'],
        });
      })
  }

  submit(): void {
    if (this.isEdit) {
      // this.update()
    } else {
      this.create();
    }
  }
}