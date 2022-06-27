import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { UtilityService } from 'src/app/Service/Hospital/all.service';

class ReqModel {
  constructor() {
    this.type = ""
    this.amount = 0;
  }
  id?: number;
  amount: number;
  type?: string;
}

@Component({
  selector: 'app-service-create',
  templateUrl: './create-service.component.html',
  styleUrls: ['./create-service.component.scss']
})
export class CreateServiceComponent implements OnInit {
  isEdit: boolean = false;
  userDetails: any[] = [];
  balance: any;
  serviceReqModel: ReqModel = new ReqModel();

  constructor(
    private untiService: UtilityService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    public toaster: MatSnackBar,
  ) { }


  ngOnInit(): void {
    this.getOpenBalance();
  }

  getOpenBalance() {
    this.untiService.GetBalaceDetails()
      .subscribe(resp => {
        this.balance = resp;
      })
  }

  create(): void {
    if (this.serviceReqModel.type === 'WITHDRAW') {
      if (this.serviceReqModel?.amount > (this.balance?.balance)) {
        alert("you dont have much balance");
        return;
      }
    }
    this.untiService.ReqService(this.serviceReqModel)
      .subscribe((resp) => {
        this.toaster.open("Create Successfully", '', {
          duration: 5000,
          verticalPosition: 'top',
          panelClass: ['green-snackbar'],
        });
        this.router.navigate(["/pages/service/index"])
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