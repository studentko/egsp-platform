import { Component, OnInit } from '@angular/core';
import { RegisterModel } from '../models/register-model'
import { Router } from "@angular/router"
import { UserService } from '../user.service'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user : RegisterModel = new RegisterModel();

  errMsg : string;

  constructor(private router : Router, private userService : UserService) { }

  ngOnInit() {
  }

  register() : void {
    this.userService.register(this.user).subscribe(aaa =>{
      if(aaa === null) {
        this.router.navigate(['/home']);
      }
      else{
        this.errMsg = "Failed to register";
      }
    })
  }

}
