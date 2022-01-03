using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DTOs
{
    public class PlayerRandomCard
    {
        public int PlayerType { get; set; } //based on PlayerTypesEnum
        public string Card { get; set; }
    }
}
