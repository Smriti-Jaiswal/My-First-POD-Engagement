import { Injectable } from '@angular/core';  
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';  
import { Observable } from 'rxjs';  
import { AuthService } from '../Service/Auth/auth.service';
  
@Injectable()  
export class JwtInterceptor implements HttpInterceptor {  
    constructor( private authenticationService: AuthService) { }  
  
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {  
        let currentUser = this.authenticationService.currentUserValue;  
        if (currentUser) {  
            request = request.clone({  
                setHeaders: {  
                    Authorization: `Bearer ${currentUser}`
                }  
            });  
        }  
        return next.handle(request);  
    }  
} 