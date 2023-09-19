using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.game.exceptions
{
    public class BlackJackWin : Exception
    {
        // true: osztó, false: player
        private bool winner = true;

        public BlackJackWin(bool winner) {
            this.winner = winner;
        }

        public bool isWinner() { return winner; }

        public override string ToString()
        {
            return $"A nyertes: {(winner ? "Osztó" : "Te" )}";
        }
    }
}
