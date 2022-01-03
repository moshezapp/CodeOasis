import { GamePlayResponseDTO } from './../../DTOs/GamePlayResponseDTO';
import { GamePlayService } from './../../services/game-play.service';
import { AfterViewInit, Component, OnInit } from '@angular/core';
import { PlayerTypesEnum } from 'src/app/Enums/PlayerTypesEnum';

@Component({
  selector: 'app-dealer',
  templateUrl: './dealer.component.html',
  styleUrls: ['./dealer.component.css']
})
export class DealerComponent implements OnInit {

  public moneyLeft: number = 0;
  public flippedState = "flipped";
  public hiddenCardImg = "card-back.jpg";

  public cards: string[] = [];

  constructor(
    private _gamePlayService: GamePlayService
  ) { }

  ngOnInit() {
    this._gamePlayService.eventEmitter$.subscribe(data => {
      this.moneyLeft = data.cashier[Number(PlayerTypesEnum.Dealer)] || this.moneyLeft;
      this.cards = this.cards.concat(this.getMyCardsFromResponse(data));

      if (this.cards.length > 1) {
        this.FlipHiddenCard();
      }

      if (this._gamePlayService.finishRound)
        this.FinishRound();
    });
  }

  getMyCardsFromResponse(data: GamePlayResponseDTO) {
    return data.playerRandomCard.filter(card => card.playerType == Number(PlayerTypesEnum.Dealer)).map(card => this._gamePlayService.ConvertToImageFileName(card.card))
  }

  FinishRound() {
    setTimeout(() => {
      this.cards = [];
      this.hiddenCardImg = "card-back.jpg";
    }, 5000);
  }

  FlipHiddenCard() {
    this.hiddenCardImg = this.cards.pop();
  }
}
