import { Injectable } from '@angular/core';
import { User } from './user'
import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http'
import { catchError, tap, map } from "rxjs/operators"
import { of, Observable } from 'rxjs';
import { UserService } from './user.service';
import { Line } from './line';

@Injectable({
  providedIn: 'root'
})
export class LineService {

  constructor(private httpClient : HttpClient,
    private userService : UserService) { }

  //TODO
  getLines(): Observable<Line[]> {
    return of([]);
  }
}
