import { Component, OnInit } from '@angular/core';
import { User } from '../user'
import { Router } from "@angular/router"
import { UserService } from '../user.service'

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  user : User = new User();

  constructor(private router : Router, private userService : UserService) { }

  ngOnInit() {
  }

  register() : void {
    if(this.userService.register(this.user)){
      this.router.navigate(['/home']);
    }
  }

}
