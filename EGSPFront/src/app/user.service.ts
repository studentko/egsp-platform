import { Injectable } from '@angular/core';
import { User } from './user'
import { RegisterModel } from "./models/register-model"
import { LoginModel } from "./models/login-model"
import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http'
import { catchError, tap, map } from "rxjs/operators"
import { of, Observable } from 'rxjs';
import { PasswordChangeModel } from './models/password-change-model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  activeUser : User;

  isLoggedIn() : boolean {
    return this.activeUser != undefined;
  }

  tokenHeader = new HttpHeaders();

  constructor(private httpClient : HttpClient) { 
    let token = sessionStorage.getItem('token');
    if(token){
      this.activeUser = new User();
      this.activeUser.token = token;
      this.tokenHeader = this.tokenHeader.append("Authorization", this.activeUser.token);
      this.getUserInfo();
    }
  }

  getUserInfo() : void {
    this.httpClient.get<string[]>('http://localhost:52295/api/Account/Roles', {headers: this.tokenHeader})
    .subscribe(data => {
      if(data[0] == 'AppUser'){
        this.activeUser.Role = "AppUser";
        this.httpClient.get<any>('http://localhost:52295/api/Customer', {headers: this.tokenHeader})
        .subscribe(data => Object.assign(this.activeUser, data));
      }
      else{
        this.activeUser.Role = data[0];
        this.httpClient.get<any>('http://localhost:52295/api/Account/UserInfo', {headers: this.tokenHeader})
        .subscribe(data => this.activeUser.Email = data.Email);
      }
    })
  }

  register(userData: RegisterModel) : Observable<any> {
    return this.httpClient.post('http://localhost:52295/api/Account/Register', userData)
    .pipe(
      catchError((err : HttpErrorResponse) => {
        return of(err);
      }));
  }

  login(userData : LoginModel) : Observable<any>  {

      let headers = new HttpHeaders();
    headers = headers.set("Content-Type", "application/x-www-form-urlencoded");

    let req = "grant_type=password&username=" + userData.Email + "&password=" + userData.Password;

    return this.httpClient.post<any>('http://localhost:52295/oauth/token', req, {headers: headers})
    .pipe(
      tap(res => {
        this.activeUser = new User();
        this.activeUser.token = "Bearer " + res.access_token;
        sessionStorage.setItem('token', this.activeUser.token);
        this.tokenHeader.delete("Authorization");
        this.tokenHeader.append("Authorization", this.activeUser.token);
        this.getUserInfo();
      }),
      catchError((err : HttpErrorResponse) => {
        return of(err);
      })
    );
  }

  getCustomerTypes() : Observable<any> {
    return this.httpClient.get<any>('http://localhost:52295/api/Customer/CustomerTypes', {headers: this.tokenHeader});
  }

  changePassword(userData : PasswordChangeModel) : Observable<any> {
    return this.httpClient.post('http://localhost:52295/api/Account/ChangePassword', userData, {headers: this.tokenHeader})
    .pipe(
      catchError((err : HttpErrorResponse) => {
        return of(err);
      }));
  }

  updateInfo(userData : RegisterModel) : Observable<any> {
    return this.httpClient.put('http://localhost:52295/api/Customer', userData, {headers: this.tokenHeader})
    .pipe(
      catchError((err : HttpErrorResponse) => {
        return of(err);
      }));
  }

  //TODO add http request
  logout() : void {
    this.activeUser = null;
    sessionStorage.clear();
  }
}
