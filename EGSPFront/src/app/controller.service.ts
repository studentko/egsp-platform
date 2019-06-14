import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { UserService } from './user.service';
import { Ticket } from './models/ticket';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { User } from './user';

@Injectable({
  providedIn: 'root'
})
export class ControllerService {

  constructor(private httpClient: HttpClient,
    private userService: UserService) { }

  pendingUsers: User[] = [];

  checkTicket(ticket: Ticket) : Observable<any> {
    return this.httpClient.post<any>('http://localhost:52295/api/Ticket/TicketCheck', 
    {TicketId: ticket.Id, AnonymousCustomerId: ticket.AnonymousCustomerId}, {headers: this.userService.tokenHeader})
    .pipe(
      catchError((err: HttpErrorResponse) => {
        return of({IsValid: false, Message: "Server/connection error, try again later"});
      })
    )
  }

  getPendingUsers() : Observable<any> {
    return this.httpClient.get<User[]>('http://localhost:52295/api/Customer/NotValidated', {headers: this.userService.tokenHeader})
    .pipe(
      tap(data => this.pendingUsers = data),
      catchError((err: HttpErrorResponse) => { return of([]); })
    );
  }

  confirmUser(id: number) : Observable<any>{
    return this.httpClient.put<any>('http://localhost:52295/api/Customer/MakeValid/' + id, {}, {headers: this.userService.tokenHeader})
  }

  denyUser(id: number) : Observable<any>{
    return this.httpClient.put<any>('http://localhost:52295/api/Customer/MakeDenied/' + id, {}, {headers: this.userService.tokenHeader})
  }
}
