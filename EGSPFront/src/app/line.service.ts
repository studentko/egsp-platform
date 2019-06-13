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
    return this.httpClient.get<Line[]>('http://localhost:52295/api/BusLine').pipe(
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

  addLine(line: Line) : Observable<any> {
    return this.httpClient.post<Line>('http://localhost:52295/api/BusLine', line, {headers: this.userService.tokenHeader})
    .pipe(
      tap(data => {
        this.getLines().subscribe();
      }),
      catchError((err: HttpErrorResponse) => {
        return of({IsSuccess: false, ErrorMessage: err.message});
      })
    );
  }

  editLine(line: Line) : Observable<any> {
    return this.httpClient.put<Line>('http://localhost:52295/api/BusLine/' + line.Id, line, {headers: this.userService.tokenHeader})
    .pipe(
      tap(data => {
        this.getLines().subscribe();
      }),
      catchError((err: HttpErrorResponse) => {
        return of({IsSuccess: false, ErrorMessage: err.message});
      })
    );
  }

  deleteLine(lineID: number) : Observable<any> {
    return this.httpClient.delete<any>('http://localhost:52295/api/BusLine/' + lineID, {headers: this.userService.tokenHeader})
    .pipe(
      tap(data => {
        this.getLines().subscribe();
      }),
      catchError((err: HttpErrorResponse) => {
        return of({IsSuccess: false, ErrorMessage: err.message});
      })
    );
  }
}
