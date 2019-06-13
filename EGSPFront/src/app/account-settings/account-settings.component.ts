import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account-settings',
  templateUrl: './account-settings.component.html',
  styleUrls: ['./account-settings.component.css']
})
export class AccountSettingsComponent implements OnInit {

  constructor(private userService: UserService, private router: Router) { }

  logout(): void{
    this.userService.logout();
    this.router.navigate(['/home']);
  }

  ngOnInit() {
  }

}
