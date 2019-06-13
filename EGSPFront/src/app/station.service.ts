import { Injectable } from '@angular/core';
import { User } from './user'
import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http'
import { catchError, tap, map } from "rxjs/operators"
import { of, Observable } from 'rxjs';
import { UserService } from './user.service';
import { Station } from './station';

@Injectable({
  providedIn: 'root'
})
export class StationService {

  constructor(private httpClient : HttpClient,
    private userService : UserService) { 
    }

  stations: Station[] = [];
  errMsg: string;

  //TODO
  getStations(): Observable<Station[]> {
    return this.httpClient.get<Station[]>('http://localhost:52295/api/BusStation').pipe(
      tap(data => {
        this.errMsg = null;
        this.stations = data;
      }),
      catchError((err: HttpErrorResponse) => {
        this.errMsg = err.message;
        this.stations = [];
        return of([]);
      })
    );
  }
}
