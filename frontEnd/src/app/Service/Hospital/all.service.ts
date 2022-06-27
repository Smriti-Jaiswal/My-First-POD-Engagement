import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {
  
    constructor(private http:HttpClient) {}

  UserCreate(data:any) {
    return this.http.post<any>(`${environment.apiUrl}/api/User`,data);
  }

  GetOneUser(id:any) {
    return this.http.get<any>(`${environment.apiUrl}/api/User/${id}`);
  }

   UpdateOneUser(data:any) {
    return this.http.put<any>(`${environment.apiUrl}/api/User`, data);
  }

  DeleteOneUser(id:any) {
    return this.http.delete<any>(`${environment.apiUrl}/api/User/${id}`);
  }

  UserGetAll() {
    return this.http.get<any>(`${environment.apiUrl}/api/User`);
  }

  GetBalaceDetails() {
    return this.http.get<any>(`${environment.apiUrl}/api/Transaction`);
  }

  doTransaction(data: any) {
    return this.http.post<any>(`${environment.apiUrl}/api/Transaction`, data);
  }

  getUserTransaction() {
    return this.http.get<any>(`${environment.apiUrl}/api/Transaction/GetUserTransaction`);
  }

  ReqService(data: any) {
    return this.http.post<any>(`${environment.apiUrl}/api/Service`, data);
  }

  ServiceGetAll(id:any) {
    return this.http.get<any>(`${environment.apiUrl}/api/Service/${id}`);
  }

  ApproveService(data: any) {
    return this.http.post<any>(`${environment.apiUrl}/api/Service/ApproveService`, data);
  }

  // deposite
  UserGetAllV1() {
    return this.http.get<any>(`${environment.apiUrl}/api/Deposite`);
  }

  Deposite(data: any) {
    return this.http.post<any>(`${environment.apiUrl}/api/Deposite`,data);
  }

}
