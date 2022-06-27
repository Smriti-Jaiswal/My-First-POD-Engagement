import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

    private result:HttpHeaders = new HttpHeaders();

    public currentUserSubject: BehaviorSubject<string>;
  
    public currentUser: Observable<string>;
  
    constructor(private http:HttpClient)
    {
      this.currentUserSubject = new BehaviorSubject<string>(JSON.parse(localStorage.getItem('auth') as any));
      this.currentUser = this.currentUserSubject.asObservable();
    }
  
    public get currentUserValue(): string
    {
      return this.currentUserSubject.value;
    }

  Login(data:any) {
    return this.http.post<any>(`${environment.apiUrl}/api/Auth`,data);
  }

  Register(data:any) {
    return this.http.post<any>(`${environment.apiUrl}/api/User/Post`,data);
  }


}
