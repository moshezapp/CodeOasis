using BlackJack.Common;
using BlackJack.Contracts;
using BlackJack.DTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.Implementation
{
    public class GamePlayService : IGamePlayService
    {
        private Dictionary<int, int> _cashier = new Dictionary<int, int>();
        private int _betAmount = 0;
        private const int INITIAL_MONEY_AMOUNT = 1000;
        
        private ICardService _cardService;
        private ILogger _logger;

        public GamePlayService(ICardService cardService, ILogger<GamePlayService> logger)
        {
            _cardService = cardService;
            _logger = logger;

            _cashier.Add((int)PlayerTypesEnum.Dealer, int.MaxValue);
            _cashier.Add((int)PlayerTypesEnum.Player, INITIAL_MONEY_AMOUNT);
        }

        private string PopRandomCard()
        {
            return _cardService.popRandomCard();
        }

        private bool UpdateCashier(int playerType, int amount)
        {
            if (_cashier[playerType] + amount < 0)
                return false;

            _cashier[playerType] += amount;
            return true;
        }

        private GamePlayResponseDTO PlayDealer()
        {
            GamePlayResponseDTO cardsResponse = new GamePlayResponseDTO();

            //rules:
            //if it has 17 or more, he must stand.
            //if its has 16 or less, must bet

            while (_cardService.CalculateCardsSumForOnePlayer((int)PlayerTypesEnum.Dealer) <= 16)
            {
                PlayerRandomCard playerRandomCard = cardsResponse.AddPlayerRandomCard((int)PlayerTypesEnum.Dealer, PopRandomCard());
                _cardService.UpdatePlayersCards(playerRandomCard);
            }

            return cardsResponse;
        }

        public GamePlayResponseDTO StartNewRound(int betAmount)
        {
            GamePlayResponseDTO gamePlayResponse = new GamePlayResponseDTO();
            PlayerRandomCard playerCard = null; //temporary placeholder

            if (betAmount <= 0 || betAmount > _cashier[(int)PlayerTypesEnum.Player])
            {
                gamePlayResponse.AddResponseCodes(StatusCodesEnum.InvalidAmountOfMoney);
                return gamePlayResponse;
            }
            
            _betAmount = betAmount;
            _cardService.FlushPlayersCards();

            GamePlayResponseDTO cardsResponse = new GamePlayResponseDTO();

            //generates 3 cards - one for dealer, 2 for regular player
            playerCard = cardsResponse.AddPlayerRandomCard((int)PlayerTypesEnum.Dealer, PopRandomCard());
            this._cardService.UpdatePlayersCards(playerCard);

            playerCard = cardsResponse.AddPlayerRandomCard((int)PlayerTypesEnum.Player, PopRandomCard());
            this._cardService.UpdatePlayersCards(playerCard);

            playerCard = cardsResponse.AddPlayerRandomCard((int)PlayerTypesEnum.Player, PopRandomCard());
            this._cardService.UpdatePlayersCards(playerCard);

            UpdateCashier((int)PlayerTypesEnum.Player, -betAmount);
            CalculateWinner(cardsResponse);
            cardsResponse.Cashier = _cashier;

            return cardsResponse;
        }

        public GamePlayResponseDTO HitPlayerNewCard()
        {
            GamePlayResponseDTO cardsResponse = new GamePlayResponseDTO();
            PlayerRandomCard playerRandomCard = cardsResponse.AddPlayerRandomCard((int)PlayerTypesEnum.Player, PopRandomCard());
            _cardService.UpdatePlayersCards(playerRandomCard);

            CalculateWinner(cardsResponse);
            cardsResponse.Cashier = _cashier;

            return cardsResponse;
        }

        public GamePlayResponseDTO DonePlayerHitting()
        {
            GamePlayResponseDTO cardsResponse = PlayDealer();

            CalculateWinner(cardsResponse, true);
            cardsResponse.Cashier = _cashier;
             
            return cardsResponse;
        }

        private void CalculateWinner(GamePlayResponseDTO cardsResponse, bool playerFinishedHitting = false)
        {
            int dealerSum = _cardService.CalculateCardsSumForOnePlayer((int)PlayerTypesEnum.Dealer);
            int playerSum = _cardService.CalculateCardsSumForOnePlayer((int)PlayerTypesEnum.Player);

            //rules:
            //the first to bust 21 - the first to lose
            //the first to reach 21 - the first to win
            //when player and dealer are even - dealer wins

            if (dealerSum > 21 || playerSum == 21)
            {
                cardsResponse.AddResponseCodes(StatusCodesEnum.PlayerIsWinner);
                UpdateCashier((int)PlayerTypesEnum.Player, _betAmount);
                UpdateCashier((int)PlayerTypesEnum.Dealer, -_betAmount);
                _cardService.FlushPlayersCards();
            }
            else if (playerSum > 21 || dealerSum == 21)
            {
                cardsResponse.AddResponseCodes(StatusCodesEnum.DealerisWinner);
                UpdateCashier((int)PlayerTypesEnum.Player, -_betAmount);
                UpdateCashier((int)PlayerTypesEnum.Dealer, _betAmount);
                _cardService.FlushPlayersCards();
            }
            else if (playerFinishedHitting && dealerSum < 21 && playerSum < 21)
            {
                if(dealerSum >= playerSum)
                {
                    cardsResponse.AddResponseCodes(StatusCodesEnum.DealerisWinner);
                    UpdateCashier((int)PlayerTypesEnum.Player, -_betAmount);
                    UpdateCashier((int)PlayerTypesEnum.Dealer, _betAmount);
                    _cardService.FlushPlayersCards();
                }
                else
                {
                    cardsResponse.AddResponseCodes(StatusCodesEnum.PlayerIsWinner);
                    UpdateCashier((int)PlayerTypesEnum.Player, _betAmount);
                    UpdateCashier((int)PlayerTypesEnum.Dealer, -_betAmount);
                    _cardService.FlushPlayersCards();
                }
            }
        }
    }
}
