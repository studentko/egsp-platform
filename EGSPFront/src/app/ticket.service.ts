import { Injectable } from '@angular/core';
import { of, Observable, from } from 'rxjs';
import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http'
import { catchError, tap, map } from "rxjs/operators"
import { UserService } from './user.service'
import { Ticket } from './models/ticket';

@Injectable({
  providedIn: 'root'
})
export class TicketService {

  constructor(private httpClient : HttpClient,
    private userService : UserService) { }

  tickets: Ticket[];
  
  getCardTypes() : Observable<string[]> {
    return this.httpClient.get<string[]>('http://localhost:52295/api/Ticket/TicketTypes', {headers: this.userService.tokenHeader});
  }

  buyTicket(type: string) : Observable<any> {
    if(this.userService.isLoggedIn()){
    return this.httpClient.post<any>('http://localhost:52295/api/Ticket/Buy', 
    {TicketType: type}, {headers: this.userService.tokenHeader} );
    }
  }

  getPurchasedTickets() : Observable<Ticket[]> {
    if(this.userService.isLoggedIn()){
      return this.httpClient.get<Ticket[]>('http://localhost:52295/api/Ticket', {headers: this.userService.tokenHeader})
      .pipe(
        tap(data => this.tickets = data)
      );
    }
  }
}
