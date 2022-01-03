using BlackJack.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DTOs
{
    public class GamePlayResponseDTO
    {
        public List<PlayerRandomCard> PlayerRandomCard { get; set; }
        public List<StatusReponse> StatusResponseCodes { get; set; }
        public Dictionary<int, int> Cashier { get; set; }

        public GamePlayResponseDTO()
        {
            PlayerRandomCard = new List<PlayerRandomCard>();
            StatusResponseCodes = new List<StatusReponse>();
            Cashier = new Dictionary<int, int>();
        }

        public void AddResponseCodes(StatusCodesEnum statusCodeEnum)
        {
            StatusResponseCodes.Add(new StatusReponse(statusCodeEnum));
        }

        public PlayerRandomCard AddPlayerRandomCard(int PlayerType,string randomCard)
        {
            PlayerRandomCard playerRandomCard = new PlayerRandomCard()
            {
                PlayerType = PlayerType,
                Card = randomCard
            };

            PlayerRandomCard.Add(playerRandomCard);

            return playerRandomCard;
        }

        public void AddPlayerRandomCard(int PlayerType, params string[] randomCards)
        {
            foreach (var card in randomCards)
            {
                PlayerRandomCard.Add(new DTOs.PlayerRandomCard()
                {
                    PlayerType = PlayerType,
                    Card = card
                });
            }
        }
    }
}
