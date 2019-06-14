import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { Router } from '@angular/router';
import { PasswordChangeModel } from '../models/password-change-model';
import { HttpErrorResponse } from '@angular/common/http';
import { RegisterModel } from '../models/register-model';

@Component({
  selector: 'app-account-settings',
  templateUrl: './account-settings.component.html',
  styleUrls: ['./account-settings.component.css']
})
export class AccountSettingsComponent implements OnInit {


  passwordChange : PasswordChangeModel = new PasswordChangeModel();
  errMsg : string;

  changeData : RegisterModel;
  errMsg2 : string;

  fileToUpload: File = null;

  constructor(private userService: UserService, private router: Router) { }

  logout(): void{
    this.userService.logout();
    this.router.navigate(['/home']);
  }

  ngOnInit() {
    this.changeData = new RegisterModel();
    this.changeData.Name = this.userService.activeUser.Name;
    this.changeData.LastName = this.userService.activeUser.LastName;
    this.changeData.Birthday = this.userService.activeUser.Birthday;
    this.changeData.Address = this.userService.activeUser.Address;
  }

  changePassword() : void {
    this.userService.changePassword(this.passwordChange).subscribe(aaa => {
      if(aaa instanceof HttpErrorResponse){
        this.errMsg = "Not valid password, check and try again";
      }
      else{
        this.router.navigate(['/home']);
      }
    });
  }

  updateInfo() : void {
    this.userService.updateInfo(this.changeData).subscribe(aaa => {
      if(aaa instanceof HttpErrorResponse){
        this.errMsg2 = "Not good personal info";
      }
      else{
        this.userService.activeUser.Name = this.changeData.Name;
        this.userService.activeUser.LastName = this.changeData.LastName;
        this.userService.activeUser.Birthday = this.changeData.Birthday;
        this.userService.activeUser.Address = this.changeData.Address;
        this.router.navigate(['/home']);
      }
    });
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }

  uploadFileToActivity() {
    this.userService.postFile(this.fileToUpload).subscribe(data => {
      this.router.navigate(['/home']);
      }, error => {
        console.log(error);
      });
  }

}
