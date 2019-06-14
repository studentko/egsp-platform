import { Component, OnInit } from '@angular/core';
import { Ticket } from '../models/ticket';
import { ControllerService } from '../controller.service';
import { of } from 'rxjs';

@Component({
  selector: 'app-controller',
  templateUrl: './controller.component.html',
  styleUrls: ['./controller.component.css']
})
export class ControllerComponent implements OnInit {

  constructor(private controllerService: ControllerService) { }

  ticket: Ticket = new Ticket();

  errMsg: string;
  passMsg: string;

  ngOnInit() {
  }

  checkTicket(): void{
    this.controllerService.checkTicket(this.ticket).subscribe(data => {
      if(data.IsValid){
        this.errMsg = null;
        this.passMsg = "Checked at " + data.Ticket.CheckinTime;
      }
      else{
        this.passMsg = null;
        this.errMsg = data.Message;
      }
    });
  }
}
