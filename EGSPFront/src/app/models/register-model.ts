export class RegisterModel {
    Email : string;
    Password : string;
    ConfirmPassword : string;
    Name : string = "";
    LastName : string = "";
    Birthday : Date;
    Address : string = "";
    CustomerTypeId : number = 1;
}
