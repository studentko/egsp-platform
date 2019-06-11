import { Component, OnInit } from '@angular/core';
import { LoginModel } from '../models/login-model'
import { Router } from "@angular/router"
import { UserService } from '../user.service'
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user : LoginModel = new LoginModel();

  errMsg : string;

  constructor(private router : Router, private userService : UserService) { }

  ngOnInit() {
  }

  login() : void {
    this.userService.login(this.user).subscribe(aaa => {
      if(aaa instanceof HttpErrorResponse){
        this.errMsg = "Failed to login";
      }
      else{
        this.router.navigate(['/home']);
      }
    });
  }

}
