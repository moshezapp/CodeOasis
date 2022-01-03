using BlackJack.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.Contracts
{
    public interface ICardService 
    {
        void FlushPlayersCards();
        int CalculateCardsSumForOnePlayer(int playerType);
        PlayerRandomCard UpdatePlayersCards(PlayerRandomCard playerRandomCard);
        string popRandomCard();
    }
}
