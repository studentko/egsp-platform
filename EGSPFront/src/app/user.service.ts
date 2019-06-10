import { Injectable } from '@angular/core';
import { User } from './user'
import { HttpClient } from '@angular/common/http'

@Injectable({
  providedIn: 'root'
})
export class UserService {

  activeUser : User;

  constructor(private httpClient : HttpClient) { }

  register(userData: User){
    this.httpClient.post<User>('http://localhost:52295/api/Account/Register', userData)
    .subscribe(au => this.activeUser = au);
  }

  //TODO add http request
  login(userData : User) : boolean {
    this.activeUser = userData;
    return true;
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
