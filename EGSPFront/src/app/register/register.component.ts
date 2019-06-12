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
  types = [];

  errMsg : string;

  constructor(private router : Router, private userService : UserService) { }

  ngOnInit() {
    this.getTypes();
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

  getTypes() : void {
    this.userService.getCustomerTypes().subscribe(data => this.types = data);
  }

}
