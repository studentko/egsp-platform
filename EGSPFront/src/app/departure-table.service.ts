import { Injectable } from '@angular/core';
import { UserService } from './user.service';
import { DepartureTable } from './departure-table';
import { Observable, of } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { HttpErrorResponse, HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DepartureTableService {

  constructor(private httpClient : HttpClient,
    private userService : UserService) { }

  tables: DepartureTable[] = [];
  errMsg: string;

  getTables(): Observable<DepartureTable[]> {
    return this.httpClient.get<DepartureTable[]>('http://localhost:52295/api/DepartureTable').pipe(
      tap(data => {
        this.errMsg = null;
        this.tables = data;
      }),
      catchError((err: HttpErrorResponse) => {
        this.errMsg = err.message;
        this.tables = [];
        return of([]);
      })
    );
  }

  addTable(table: DepartureTable) : Observable<any> {
    return this.httpClient.post<DepartureTable>('http://localhost:52295/api/DepartureTable', 
    table, {headers: this.userService.tokenHeader})
    .pipe(
      tap(data => {
        this.getTables().subscribe();
      }),
      catchError((err: HttpErrorResponse) => {
        return of({IsSuccess: false, ErrorMessage: err.message});
      })
    );
  }

  editTable(table: DepartureTable) : Observable<any> {
    return this.httpClient.put<DepartureTable>('http://localhost:52295/api/DepartureTable/' + table.Id, 
    table, {headers: this.userService.tokenHeader})
    .pipe(
      tap(data => {
        this.getTables().subscribe();
      }),
      catchError((err: HttpErrorResponse) => {
        return of({IsSuccess: false, ErrorMessage: err.message});
      })
    );
  }

  deleteTable(tableID: number) : Observable<any> {
    return this.httpClient.delete<any>('http://localhost:52295/api/DepartureTable/' + tableID, {headers: this.userService.tokenHeader})
    .pipe(
      tap(data => {
        this.getTables().subscribe();
      }),
      catchError((err: HttpErrorResponse) => {
        return of({IsSuccess: false, ErrorMessage: err.message});
      })
    );
  }
}
