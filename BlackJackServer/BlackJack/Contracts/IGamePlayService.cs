using BlackJack.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.Contracts
{
    public interface IGamePlayService
    {
        GamePlayResponseDTO StartNewRound(int betAmount);
        GamePlayResponseDTO HitPlayerNewCard();
        GamePlayResponseDTO DonePlayerHitting();
    }
}
