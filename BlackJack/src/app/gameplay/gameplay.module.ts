import { ServicesModule } from './../services/services.module';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DealerComponent } from './dealer/dealer.component';
import { PlayerComponent } from './player/player.component';
import { HttpClientModule } from '@angular/common/http';
import { GameCardComponent } from './game-card/game-card.component';

@NgModule({
  declarations: [DealerComponent, PlayerComponent, GameCardComponent],
  imports: [
    CommonModule,    
    ServicesModule,
    HttpClientModule,
    ServicesModule
  ],
  exports:[
    PlayerComponent ,
    DealerComponent,
    GameCardComponent
  ]
})
export class GameplayModule { }
