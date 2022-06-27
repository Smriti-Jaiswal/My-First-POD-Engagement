import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { UtilityService } from 'src/app/Service/Hospital/all.service';

class UserReqModel {
  constructor() {
    this.gender = "Male"
  }
  id?: number;
  firstName?: string;
  lastName?: string;
  gender?: string;
  dateOfBirth?: string;
  address?: string;
  city?: string;
  country?: string;
  email?: string;
  password?: string;
  accountNumber?: string;
  openAmount?: number;
  userType?: string;
}

@Component({
  selector: 'app-user-create',
  templateUrl: './create-user.component.html',
  styleUrls: ['./create-user.component.scss']
})
export class CreateUserComponent implements OnInit {
  isEdit: boolean = false;

  userReqModel: UserReqModel = new UserReqModel();

  constructor(
    private untiService: UtilityService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    public toaster: MatSnackBar,
  ) { }


  ngOnInit(): void {
    this.activatedRoute.params
      .subscribe((params) => {
        if (params.id) {
          this.isEdit = true;
          this.getOne(params.id);
        }
      });
  }

  getOne(id:any): void {
    this.untiService.GetOneUser(id)
      .subscribe((resp) => {
        this.userReqModel = resp.model;
      }, (err) => {
        this.toaster.open(err.error.message, '', {
          duration: 5000,
          verticalPosition: 'top',
          panelClass: ['red-snackbar'],
        });
      })
  }


  create(): void {
    this.userReqModel.userType = 'User';
    this.untiService.UserCreate(this.userReqModel)
      .subscribe((resp) => {
        this.toaster.open("Create Successfully", '', {
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

  update(): void {
    this.userReqModel.userType = 'User';
    this.untiService.UpdateOneUser(this.userReqModel)
      .subscribe((resp) => {
        this.toaster.open("Update Successfully", '', {
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
      this.update()
    } else {
      this.create();
    }
  }
}