using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.game.exceptions
{
    public class PushWin : Exception
    {
        public PushWin() { }

        public override string ToString()
        {
            return Program.GAME_PREFIX + "Gratulálunk! Az osztó és a te lapjaid értéke is ugyan azok voltak!";
        }
    }
}
