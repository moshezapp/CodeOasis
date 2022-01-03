import { GamePlayService } from './../../services/game-play.service';
import { Component, OnInit } from '@angular/core';
import { PlayerTypesEnum } from 'src/app/Enums/PlayerTypesEnum';

@Component({
  selector: 'app-player',
  templateUrl: './player.component.html',
  styleUrls: ['./player.component.css']
})
export class PlayerComponent implements OnInit {

  public moneyLeft: number = 0;
  public cards: string[] = [];
  public flippedState = "flipped";
  
  public afterBetting = false;
  public finishRound = true;

  constructor(
    private _gamePlayService: GamePlayService
  ) { }

  ngOnInit() {
    this._gamePlayService.eventEmitter$.subscribe(data => {
      this.moneyLeft = data.cashier[Number(PlayerTypesEnum.Player)] || this.moneyLeft;
      this.cards = this.cards.concat(data.playerRandomCard.filter(card => card.playerType == Number(PlayerTypesEnum.Player)).map(card => this._gamePlayService.ConvertToImageFileName(card.card)));

      if (this._gamePlayService.finishRound)
        this.FinishRound()
    });
  }

  FinishRound() {
    this.finishRound = false;
    this.afterBetting = false;

    setTimeout(() => {
      this.finishRound = true;
      this.afterBetting = false;
      this.cards = [];
    } , 5000);
  }

  Bet(betAmount: number) {
    this._gamePlayService.StartNewRound(betAmount);
    this.afterBetting = true;
    this.finishRound = false;
  }

  Hit() {
    this._gamePlayService.HitPlayerNewCard();
  }

  Done() {
    this._gamePlayService.DonePlayerHitting();
  }
}
