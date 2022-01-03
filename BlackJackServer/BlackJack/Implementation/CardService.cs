using BlackJack.Common;
using BlackJack.Contracts;
using BlackJack.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.Implementation
{
    public class CardService : ICardService
    {
        private Dictionary<string, int> _cardsBox;
        private Dictionary<int, List<string>> _playersCards;
        private Random _rand = new Random((int)DateTime.Today.Ticks);        
        private int CARDS_BOX_HALF_LENGTH = (Enum.GetValues(typeof(CARD_VALUES)).Length * Enum.GetValues(typeof(CARD_MARKS)).Length) / 2;
        private const int MAX_WINNING_VALUE = 21;

        public CardService()
        {
            createCardsBox();

            _cardsBox = new Dictionary<string, int>();

            _playersCards = new Dictionary<int, List<string>>();
            _playersCards.Add((int)PlayerTypesEnum.Dealer, new List<string>());
            _playersCards.Add((int)PlayerTypesEnum.Player, new List<string>());
        }

        //creates box that contains FIVE full packs of cards (to increase randomally against card-counters)
        private void createCardsBox()
        {
            _cardsBox = new Dictionary<string, int>(); //empty the cards box

            //iterate over 2 Enums (card value & card mark) to get total list of cards
            foreach (var value in Enum.GetNames(typeof(CARD_VALUES)))
                foreach (var mark in Enum.GetNames(typeof(CARD_MARKS)))
                    _cardsBox.Add(value.ToString() + "-" + mark.ToString(), 5);
        }

        public PlayerRandomCard UpdatePlayersCards(PlayerRandomCard playerRandomCard)
        {
            _playersCards.TryAdd(playerRandomCard.PlayerType, new List<string>());
            _playersCards[playerRandomCard.PlayerType].Add(playerRandomCard.Card);

            return playerRandomCard;
        }

        //pops random (pseudo) card. to make it more secure need to use real random numbers (that passed Die Hard test)
        public string popRandomCard()
        {
            //checks if the CardsBox is less than half of its capacity, so we need to re-generate it to make it more secure against card-counters.
            if (_cardsBox.Count <= CARDS_BOX_HALF_LENGTH)
                createCardsBox();

            //gets random key from the card box
            string randomKey = _cardsBox.ElementAt(_rand.Next(0, _cardsBox.Count)).Key;


            _cardsBox[randomKey]--;
            if (_cardsBox[randomKey] == 0)
                _cardsBox.Remove(randomKey);

            return randomKey;
        }

        public void FlushPlayersCards()
        {
            _playersCards = new Dictionary<int, List<string>>();
            _playersCards.Add((int)PlayerTypesEnum.Dealer, new List<string>());
            _playersCards.Add((int)PlayerTypesEnum.Player, new List<string>());
        }

        public int CalculateCardsSumForOnePlayer(int playerType)
        {
            int AcesCounter = 0; //how much ACE cards 
            int cardsSum = 0;
            string currentCard = string.Empty;

            foreach (var card in _playersCards[playerType])
            {
                currentCard = card.Substring(0, 2); //get the card portion from the string

                if (currentCard == "_A")
                    AcesCounter++;

                if (currentCard == "_1") //specific fix for "_10" card
                    currentCard = "_10";

                //convert the card string name to its equivalent value in BlackJack as configured in the enum
                cardsSum += (int)Enum.Parse(typeof(CARD_VALUES), currentCard); 
            }

            //if player have more than 21 in total - need to try to decrease the value of Aces from 11 to 1
            while(cardsSum > MAX_WINNING_VALUE)
            {
                if (AcesCounter == 0)
                    break;

                AcesCounter--;
                cardsSum -= 10;
            }

            return cardsSum;
        }

    }
}
