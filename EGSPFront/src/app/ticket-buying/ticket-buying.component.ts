import { Component, OnInit } from '@angular/core';
import { TicketService } from '../ticket.service'
import { UserService } from '../user.service'

@Component({
  selector: 'app-ticket-buying',
  templateUrl: './ticket-buying.component.html',
  styleUrls: ['./ticket-buying.component.css']
})
export class TicketBuyingComponent implements OnInit {

  constructor(private ticketService: TicketService, private userService: UserService) { }

  ticketTypes: string[];
  errMsg: string;

  ngOnInit() {
    this.getTicketTypes();
    //this.ticketTypes = ['test1', 'test2'];
  }

  getTicketTypes() : void {
    this.ticketService.getCardTypes().subscribe(data => this.ticketTypes = data);
  }

  buyTicket(type: string) : void{
    this.ticketService.buyTicket(type).subscribe(
      data =>{
        if(data.IsSuccess){
          this.errMsg = null;
        }
        else{
          this.errMsg = data.ErrorMessage;
        }
      }
    );
  }

}
