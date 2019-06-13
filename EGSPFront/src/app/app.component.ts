import { Component } from '@angular/core';
import { UserService } from './user.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  title = 'EGSP';

  constructor(private userService : UserService) {}

  logout() : void {
    this.userService.activeUser.Email = "123";
    this.userService.logout();
  }
}
