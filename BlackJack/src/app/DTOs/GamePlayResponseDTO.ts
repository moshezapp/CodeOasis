import { StatusReponseDTO } from './StatusReponseDTO';
import { PlayerRandomCardDTO } from './PlayerRandomCardDTO';

export class GamePlayResponseDTO {
    playerRandomCard : PlayerRandomCardDTO[] = [];
    statusResponseCodes : StatusReponseDTO[] = [];
    cashier : { [key: number]: number; } = {};
}