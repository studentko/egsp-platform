import { Component, OnInit } from '@angular/core';
import { TicketService } from '../ticket.service'
import { UserService } from '../user.service'
import { Ticket } from '../models/ticket';

@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.css']
})
export class TicketsComponent implements OnInit {

  constructor(private ticketService: TicketService, private userService: UserService) { }

  errMsg: string;

  ngOnInit() {
    this.ticketService.getPurchasedTickets().subscribe();
  }

  checkIn(ticket: Ticket) : void {
    this.ticketService.checkIn(ticket).subscribe(
      data =>{
        if(data.IsSuccess){
          this.errMsg = null;
        }
        else{
          this.errMsg = data.ErrorMessage;
        }
    });
  }
}
