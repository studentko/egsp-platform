import { Injectable } from '@angular/core';
import { User } from './user'
import { HttpClient, HttpErrorResponse, HttpParams, HttpHeaders } from '@angular/common/http'
import { catchError, tap, map } from "rxjs/operators"
import { of, Observable } from 'rxjs';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class StationService {

  constructor(private httpClient : HttpClient,
    private userService : UserService) { }
}
