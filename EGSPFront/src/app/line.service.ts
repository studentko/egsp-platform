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

  lines: Line[] = [];
  errMsg: string;

  getLines(): Observable<Line[]> {
    return this.httpClient.get<Line[]>('http://localhost:52295/api/BusStation').pipe(
      tap(data => {
        this.errMsg = null;
        this.lines = data;
      }),
      catchError((err: HttpErrorResponse) => {
        this.errMsg = err.message;
        this.lines = [];
        return of([]);
      })
    );
  }
}
