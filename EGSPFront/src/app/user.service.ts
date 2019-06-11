import { Injectable } from '@angular/core';
import { User } from './user'
import { RegisterModel } from "./models/register-model"
import { LoginModel } from "./models/login-model"
import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http'
import { catchError, tap, map } from "rxjs/operators"
import { of, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  activeUser : User;

  lHeaders = new HttpHeaders();

  constructor(private httpClient : HttpClient) { 
    let temp = sessionStorage.getItem('user');
    if(temp){
      this.activeUser = JSON.parse(temp);
      this.lHeaders.append("Authorization", this.activeUser.token);
    }
  }

  register(userData: RegisterModel) : Observable<any> {
    return this.httpClient.post('http://localhost:52295/api/Account/Register', userData)
    .pipe(
      catchError((err : HttpErrorResponse) => {
        return of(err);
      }));
  }

  //TODO add http request
  login(userData : LoginModel) : Observable<any>  {

      let headers = new HttpHeaders();
    headers = headers.set("Content-Type", "application/x-www-form-urlencoded");

    let req = "grant_type=password&username=" + userData.Email + "&password=" + userData.Password;

    return this.httpClient.post<any>('http://localhost:52295/oauth/token', req, {headers: headers})
    .pipe(
      tap(res => {
        this.activeUser = new User();
        this.activeUser.Email = userData.Email;
        this.activeUser.token = "Bearer " + res.access_token;
        sessionStorage.setItem('user', JSON.stringify(this.activeUser));
        this.lHeaders.delete("Authorization");
        this.lHeaders.append("Authorization", this.activeUser.token);
      }),
      catchError((err : HttpErrorResponse) => {
        return of(err);
      })
    );
  }

  //TODO add http request
  logout() : boolean {
    if(this.activeUser){
      this.activeUser = undefined;
      return true;
    }
    return false;
  }
}
