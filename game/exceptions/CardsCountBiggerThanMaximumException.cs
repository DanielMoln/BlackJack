using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.game.exceptions
{
    public class CardsCountBiggerThanMaximumException : Exception
    {
        public CardsCountBiggerThanMaximumException() { }

        public override string ToString()
        {
            return Program.GAME_PREFIX + " * BUST *  A kártyáid összege meghaladta a maximális értéket, így veszítettél!";
        }
    }
}
