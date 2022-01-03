import { InfoSnackBarService } from './info-snack-bar.service';
import { StatusCodesEnum } from './../Enums/StatusCodesEnum';
import { GamePlayResponseDTO } from './../DTOs/GamePlayResponseDTO';
import { HttpCallsService } from './http-calls.service';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class GamePlayService {

  private eventEmitter = new Subject<GamePlayResponseDTO>();
  public eventEmitter$ = this.eventEmitter.asObservable();
  public finishRound: boolean = false;

  constructor(
    private _httpService: HttpCallsService,
    private _infoSnackBar: InfoSnackBarService
  ) { }

  StartNewRound(betAmount: number) {
    this.finishRound = false;
    this._httpService.StartNewRound(betAmount).pipe(
      retry(1),
      catchError(this._httpService.handleError)
    )
      .subscribe(res => { this.CheckStatusCodes(res); this.eventEmitter.next(res) });
  }

  HitPlayerNewCard() {
    this._httpService.HitPlayerNewCard().pipe(
      retry(1),
      catchError(this._httpService.handleError)
    )
      .subscribe(res => { this.CheckStatusCodes(res); this.eventEmitter.next(res) });
  }

  DonePlayerHitting() {
    this._httpService.DonePlayerHitting().pipe(
      retry(1),
      catchError(this._httpService.handleError)
    )
      .subscribe(res => { this.CheckStatusCodes(res); this.eventEmitter.next(res) });
  }


  FinishRound() {
    this.finishRound = true;
  }

  CheckStatusCodes(res: GamePlayResponseDTO): boolean {
    for (let i = 0; i < res.statusResponseCodes.length; i++) {
      switch (res.statusResponseCodes.pop().statusCode) {
        case Number(StatusCodesEnum.DealerisWinner):
          this._infoSnackBar.Info("Dealer is Winner !!");
          this.FinishRound();
          break;

        case Number(StatusCodesEnum.InvalidAmountOfMoney):
          this._infoSnackBar.Info("Invalid Amount Of Money !!");
          break;

        case Number(StatusCodesEnum.NotHaveEnoughMoney):
          this._infoSnackBar.Info("Not Have Enough Money !!");
          break;

        case Number(StatusCodesEnum.PlayerIsWinner):
          this._infoSnackBar.Info("Player Is Winner !!");
          this.FinishRound();

        default:
          return true;
      }
    }
  }

  ConvertToImageFileName(shortName: string) {
    const cardTypeDict = {
      "S": "spades", //Spade
      "C": "clubs", //Clover/Club
      "H": "hearts", //Heart
      "D": "diamonds" //Diamond
    }

    let cardValue = shortName.substring(1, 2);
    if(cardValue == "1")
    return `10_of_${cardTypeDict[shortName.substring(4, 5)]}.png`;
    
    return `${cardValue}_of_${cardTypeDict[shortName.substring(3, 4)]}.png`;
  }

}