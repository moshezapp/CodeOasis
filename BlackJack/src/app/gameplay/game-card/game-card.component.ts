import { CardData } from './../CardData';
import { animate, state, style, transition, trigger } from '@angular/animations';
import { AfterViewInit, Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-game-card',
  templateUrl: './game-card.component.html',
  styleUrls: ['./game-card.component.css'],
  animations: [
    trigger('cardFlip', [
      state('default', style({
        transform: 'none'
      })),
      state('flipped', style({
        transform: 'rotateY(180deg)'
      })),
      transition('default => flipped', [
        animate('400ms')
      ]),
      transition('flipped => default', [
        animate('200ms')
      ])
    ]
    )
  ]
})
export class GameCardComponent implements OnInit {
  @Input("flipState") flipStateInput: 'default' | 'flipped' | 'matched' = "default"; //this.data.state;
  @Input() CardValueMark = "";

  flipState = 'default';
  constructor() {
  }

  ngOnInit() {
    setTimeout(() => this.flipState = this.flipStateInput, 100);
  }

}
