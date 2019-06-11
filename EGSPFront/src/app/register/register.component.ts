import { Component, OnInit } from '@angular/core';
import { User } from '../user'
import { Router } from "@angular/router"
import { UserService } from '../user.service'
import { RegisterModel } from '../models/register-model'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user = new RegisterModel();
  types = [];

  constructor(private router : Router, private userService : UserService) { }

  ngOnInit() {
    this.getTypes();
  }

  register() : void {
    this.userService.register(this.user);
      //this.router.navigate(['/home']);
  }

  getTypes() : void {
    this.userService.getCustomerTypes().subscribe(data => this.types = data);
  }

}
