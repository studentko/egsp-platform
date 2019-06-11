import { Component, OnInit } from '@angular/core';
import { User } from '../user'
import { Router } from "@angular/router"
import { UserService } from '../user.service'

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  user : User = new User();

  constructor(private router : Router, private userService : UserService) { }

  ngOnInit() {
  }

  login() : void {
    if(this.userService.login(this.user)){
      this.router.navigate(['/home']);
    }
  }

}
