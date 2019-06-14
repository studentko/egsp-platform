import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { UserService } from './user.service';
import { Ticket } from './models/ticket';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ControllerService {

  constructor(private httpClient: HttpClient,
    private userService: UserService) { }

  checkTicket(ticket: Ticket) : Observable<any> {
    return this.httpClient.post<any>('http://localhost:52295/api/Ticket/TicketCheck', 
    {TicketId: ticket.Id, AnonymousCustomerId: ticket.AnonymousCustomerId}, {headers: this.userService.tokenHeader})
    .pipe(
      catchError((err: HttpErrorResponse) => {
        return of({IsValid: false, Message: "Server/connection error, try again later"});
      })
    )
  }
}
