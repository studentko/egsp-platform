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

  addStation(station: Station) : Observable<any>{
    return this.httpClient.post<Station>('http://localhost:52295/api/BusStation', station, {headers: this.userService.tokenHeader})
    .pipe(
      tap(data => {
        this.stations.push(data);
      }),
      catchError((err: HttpErrorResponse) => {
        return of({IsSuccess: false, ErrorMessage: err.message});
      })
    );
  }

  editStation(station: Station) : Observable<any>{
    return this.httpClient.put<Station>('http://localhost:52295/api/BusStation/' + station.Id, station, {headers: this.userService.tokenHeader})
    .pipe(
      tap(data => {
        this.getStations().subscribe();
      }),
      catchError((err: HttpErrorResponse) => {
        return of({IsSuccess: false, ErrorMessage: err.message});
      })
    );
  }

  deleteStation(stationID: number) : Observable<any> {
    return this.httpClient.delete<any>('http://localhost:52295/api/BusStation/' + stationID, {headers: this.userService.tokenHeader})
    .pipe(
      tap(data => {
        this.getStations().subscribe();
      }),
      catchError((err: HttpErrorResponse) => {
        return of({IsSuccess: false, ErrorMessage: err.message});
      })
    );
  }
}
