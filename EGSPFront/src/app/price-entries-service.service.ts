import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { PriceEntry } from './models/price-entry-model';
import { Observable } from 'rxjs';
import { tap, catchError } from 'rxjs/operators';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class PriceEntriesServiceService {

  constructor(private httpClient : HttpClient,
    private userService : UserService) {}

  prices: PriceEntry[]
  errMsg: string
  priceHistory: PriceEntry[]

  getPriceEntries(): Observable<PriceEntry[]> {
    return this.httpClient.get<PriceEntry[]>('http://localhost:52295/api/PriceEntries').pipe(
      tap(data => {
        this.errMsg = null;
        this.prices = data;
      })/*,
      catchError((err: HttpErrorResponse) => {
        this.errMsg = err.message;
        this.prices = [];
        return of(this.prices);
      })*/
    );
  }

  getPriceHistory(): Observable<PriceEntry[]> {
    return this.httpClient.get<PriceEntry[]>('http://localhost:52295/api/PriceEntries/History', {headers: this.userService.tokenHeader}).pipe(
      tap(data => {
        this.errMsg = null;
        this.priceHistory = data;
      })/*,
      catchError((err: HttpErrorResponse) => {
        this.errMsg = err.message;
        this.prices = [];
        return of(this.prices);
      })*/
    );
  }

  setNewPrices(): Observable<any> {
    return this.httpClient.put<any>('http://localhost:52295/api/PriceEntries', this.prices, {headers: this.userService.tokenHeader})
    .pipe(
      tap(data => {
        this.getPriceHistory().subscribe();
        //this.getLines().subscribe();
      }),
      /*catchError((err: HttpErrorResponse) => {
        return of({IsSuccess: false, ErrorMessage: err.message});
      })*/
    );
  }
}
