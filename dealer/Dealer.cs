using BlackJack.game.exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.dealer
{
    public class Dealer
    {
        private Random random = new Random();
        private List<int> dealerCards = new List<int>();

        public bool init(List<int> cards)
        {
            // Generate cards to the dealer
            int n1 = random.Next(0, cards.Count);
            int n2 = random.Next(0, cards.Count);

            addTwoCard(new List<int>() { cards.ElementAt(n1), cards.ElementAt(n2) });

            if ((cards.ElementAt(n1) + cards.ElementAt(n2)) == 21)
            {
                throw new BlackJackWin(true);
            }

            return true;
        }

        public List<int> getDealerCards()
        {
            return dealerCards;
        }

        public void setDealerCards(List<int> cards)
        {
            dealerCards = cards;
        }

        public void addCard(int card)
        {
            if (!((getCardSum() + card) > 21))
            {
                dealerCards.Add(card);
            }
        }

        public void addTwoCard(List<int> cards)
        {
            cards.ForEach(card =>
            {
                if (!dealerCards.Contains(card))
                {
                    dealerCards.Add(card);
                }
            });
        }

        public int getCardSum()
        {
            return dealerCards.Sum();
        }

        public bool isContinue()
        {
            int r = random.Next(1, 10);

            return r > 5 ? true : false;
        }

        public override string ToString()
        {
            string nums = "";
            dealerCards.ForEach(card =>
            {
                if (card == 1 || card == 11)
                {
                    nums += "Á "; 
                }
                else
                {
                    nums += $"{card} ";
                }
            });
            return nums;
        }
    }
}
