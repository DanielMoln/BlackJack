using BlackJack.game.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.player
{
    public class Player
    {
        private Random random = new Random();
        private List<int> playerCards = new List<int>();

        private int bet;
        private bool isDoubled = false;
        private bool isStopped = false;

        public bool init(List<int> cards)
        {
            Console.Write(Program.GAME_PREFIX + "Kérlek add meg a tétet: ");
            int.TryParse(Console.ReadLine(), out bet);

            if (bet < 0)
            {
                Console.WriteLine(Program.GAME_PREFIX + "A téted helytelen, játék vége!");
                return false;
            }

            // Generate cards to the player
            int n1 = random.Next(0, cards.Count);
            int n2 = random.Next(0, cards.Count);

            addTwoCard(new List<int>() { cards.ElementAt(n1), cards.ElementAt(n2) });

            if ((cards.ElementAt(n1) + cards.ElementAt(n2)) == 21)
            {
                throw new BlackJackWin(false);
            }

            return true;
        }

        public int getBet() { return bet; }

        public List<int> getPlayerCards()
        {
            return playerCards;
        }

        public void setPlayerCards(List<int> cards)
        {
            playerCards = cards;
        }

        public bool isStop()
        {
            return isStopped;
        }

        public void setIsStop(bool stop)
        {
            isStopped = stop;
        }

        public bool isDouble()
        {
            return isDoubled;
        }

        public void setDoubled(bool doubled)
        {
            isDoubled = doubled;
        }

        public void addCard(int card)
        {
            if ((getCardSum() + card) > 21)
            {
                throw new CardsCountBiggerThanMaximumException();
            }
            else
            {
                playerCards.Add(card);
            }
        }

        public void addTwoCard(List<int> cards)
        {
            cards.ForEach(card =>
            {
                if (!playerCards.Contains(card))
                {
                    playerCards.Add(card);
                }
            });
        }

        public int getCardSum()
        {
            int sum = 0;
            playerCards.ForEach(card => sum += card);
            return sum;
        }

        public void printStats()
        {
            Console.WriteLine(Program.GAME_PREFIX + "****");
            Console.WriteLine(Program.GAME_PREFIX + "Kártyáid: " + this);
            Console.WriteLine(Program.GAME_PREFIX + "Kártyáid összege: " + getCardSum());
            Console.WriteLine(Program.GAME_PREFIX + "****");
        }

        public override string ToString()
        {
            string nums = "";
            playerCards.ForEach(card => nums += $"{card} ");
            return nums;
        }
    }
}
