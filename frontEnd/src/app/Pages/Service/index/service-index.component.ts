import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { UtilityService } from 'src/app/Service/Hospital/all.service';
import jwt_decode from "jwt-decode";

@Component({
  selector: 'app-service-index',
  templateUrl: './service-index.component.html',
  styleUrls: ['./service-index.component.scss']
})
export class ServiceIndexComponent implements AfterViewInit, OnInit {
  displayedColumns: string[] = ['reqBy', 'type', 'status', 'date'];

  dataSource = new MatTableDataSource<any>();

  constructor(
    public toaster: MatSnackBar,
    private router: Router,
    private utilService: UtilityService
  ) {

  }

  updateSts(element:any,sts: boolean): void {
    console.log(element);
    const obj:any = {
      isApproved: sts,
      id: element.id
    }
    if(element.status !== 'Pending')
    {
      alert("Pending stats only can applicable for approve");
      return;
    }
    this.utilService.ApproveService(obj)
    .subscribe((resp) => {
      this.getAll(parseInt(this.currentUserDetails.Name));
    })
  }

  applyFilter(filterValue: string) {
    filterValue = filterValue.trim(); // Remove whitespace
    filterValue = filterValue.toLowerCase(); // MatTableDataSource defaults to lowercase matches
    this.dataSource.filter = filterValue;
  }

  @ViewChild(MatPaginator) paginator: MatPaginator | any;

  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }

  getAll(id: number): void {
    this.utilService.ServiceGetAll(id)
    .subscribe((resp) => {
      this.dataSource = new MatTableDataSource<any>(resp.model);
      this.dataSource.paginator = this.paginator;
    })
  }
  currentUserDetails:any = null;
  ngOnInit(): void {
    this.currentUserDetails = jwt_decode(localStorage.getItem('auth') ?? "");
    if(this.currentUserDetails.Role === 'Admin') {
      this.displayedColumns.push('approve');
      this.displayedColumns.push('reject');
    }
    console.log(this.currentUserDetails);
    this.getAll(parseInt(this.currentUserDetails.Name));
  }
}