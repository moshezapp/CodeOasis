import { GamePlayResponseDTO } from './../DTOs/GamePlayResponseDTO';
import { InfoSnackBarService } from './info-snack-bar.service';
import { environment } from './../../environments/environment';
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpCallsService {

  _server = environment.API_SERVER_PROTOCOL + environment.API_SERVER_IP + ":" + environment.API_SERVER_PORT + "/" + environment.API_PREFIX + "/gameplay/";

  constructor(
    private _http: HttpClient,
    private _infoSnackBar: InfoSnackBarService,
  ) { }

  StartNewRound(betAmount: number) {
    return this._http.get<GamePlayResponseDTO>(this._server + `StartNewRound/${betAmount}`);
  }

  HitPlayerNewCard() {
    return this._http.get<GamePlayResponseDTO>(this._server + `HitPlayerNewCard`);
  }

  DonePlayerHitting() {
    return this._http.get<GamePlayResponseDTO>(this._server + `DonePlayerHitting`);
  }

  public handleError(unHandeledError: HttpErrorResponse): Observable<never> {
    this._infoSnackBar.Error(unHandeledError.message);

    return throwError(
      'Something bad happened; please try again later.');
  }
}
