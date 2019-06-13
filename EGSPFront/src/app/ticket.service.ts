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
    private userService : UserService) { 
    }

  tickets: Ticket[];
  
  getCardTypes() : Observable<string[]> {
    return this.httpClient.get<string[]>('http://localhost:52295/api/Ticket/TicketTypes', {headers: this.userService.tokenHeader});
  }

  buyTicket(type: string) : Observable<any> {
    if(this.userService.isLoggedIn()){
      return this.httpClient.post<any>('http://localhost:52295/api/Ticket/Buy', 
      {TicketType: type}, {headers: this.userService.tokenHeader} ).pipe(
        tap(data => {
          if(data.IsSuccess){
            this.getPurchasedTickets().subscribe();
          }
        }),
        catchError((err : HttpErrorResponse) => {
          return of({IsSuccess: false, ErrorMessage: err.message})
        })
      );
    }
    else{
      return this.httpClient.post<any>('http://localhost:52295/api/Ticket/BuyAnonymous', 
      {TicketType: type}, {headers: this.userService.tokenHeader} ).pipe(
        tap(data => {
          if(data.IsSuccess){
            this.tickets.push(data.Ticket);
            sessionStorage.setItem('tickets', JSON.stringify(this.tickets));
          }
        }),
        catchError((err : HttpErrorResponse) => {
          return of({IsSuccess: false, ErrorMessage: err.message})
        })
      );
    }
  }

  getPurchasedTickets() : Observable<Ticket[]> {
    if(this.userService.isLoggedIn()){
      return this.httpClient.get<Ticket[]>('http://localhost:52295/api/Ticket', {headers: this.userService.tokenHeader})
      .pipe(
        tap(data => this.tickets = data)
      );
    }
    else{
      this.tickets = JSON.parse(sessionStorage.getItem('tickets'));
      if(!this.tickets) this.tickets = [];
      return of(this.tickets);
    }
  }

  checkIn(ticket: Ticket) : Observable<any>{
    if(this.userService.isLoggedIn()){
      return this.httpClient.put<any>('http://localhost:52295/api/Ticket/Checkin/' + ticket.Id, {}, {headers: this.userService.tokenHeader})
      .pipe(
        tap(data => {
          if(data.IsSuccess){
            this.tickets[this.tickets.findIndex(t => t.Id === ticket.Id)] = ticket;
            this.getPurchasedTickets().subscribe();
          }
        }),
        catchError((err : HttpErrorResponse) => {
          return of({IsSuccess: false, ErrorMessage: err.message});
        })
      );
    }
    else{
      return this.httpClient.put<any>('http://localhost:52295/api/Ticket/AnonymousCheckin/' + ticket.Id, 
      {AnonymousCustomerId: ticket.AnonymousCustomerId} ,{headers: this.userService.tokenHeader})
      .pipe(
        tap(data => {
          if(data.IsSuccess){
            this.tickets[this.tickets.findIndex(t => t.Id === ticket.Id)] = data.Ticket;
            sessionStorage.setItem('tickets', JSON.stringify(this.tickets));
          }
        }),
        catchError((err : HttpErrorResponse) => {
          return of({IsSuccess: false, ErrorMessage: err.message});
        })
      );
    }
  }
}
