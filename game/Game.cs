using BlackJack.dealer;
using BlackJack.game.exceptions;
using BlackJack.player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.game
{
    public class Game
    {
        static Random random = new Random();
        static List<int> cards = new List<int>();
        public Boolean isRunning = true;

        public Game() { }

        public void run()
        {
            try
            {
                Console.WriteLine("***** BlackJack *****");
                Console.WriteLine("\n");
                Console.WriteLine("Üdvözöllek a játékban, Guest!");
                fill();

                Player player = new Player();
                bool playerInitStatus = player.init(cards);

                Dealer dealer = new Dealer();
                bool dealerInitStatus = dealer.init(cards);

                if (!playerInitStatus) return;
                if (!dealerInitStatus) return;

                Console.WriteLine(Program.GAME_PREFIX + "******");
                Console.WriteLine(Program.GAME_PREFIX + "Osztó kártyája: _ " + dealer.getDealerCards().ElementAt(0));
                Console.WriteLine(Program.GAME_PREFIX + "******");

                Console.WriteLine(Program.GAME_PREFIX + "******");
                Console.WriteLine(Program.GAME_PREFIX + "Kártyáid: " + player);
                Console.WriteLine(Program.GAME_PREFIX + "Kártyáid összege: " + player.getCardSum());
                Console.WriteLine(Program.GAME_PREFIX + "******");

                try
                {
                    do
                    {
                        gameChoices(player, dealer);
                    } while (isRunning);
                }
                catch (CardsCountBiggerThanMaximumException c)
                {
                    Console.WriteLine(c);
                    stopGame();
                }
                catch (PushWin p)
                {
                    Console.WriteLine(p);
                    stopGame();
                }
                catch (BlackJackWin c)
                {
                    Console.WriteLine(c);
                    if (!c.isWinner())
                    {
                        Console.WriteLine(Program.GAME_PREFIX + "Gratulálok! Nyereményed: " + (player.getBet() * 1.5));
                    }
                    stopGame();
                }
                catch (Exception e)
                {
                    Console.WriteLine(Program.GAME_PREFIX + "Sajnáljuk, valami hiba történt!");
                    stopGame();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(Program.GAME_PREFIX + "Sajnáljuk, valami hiba történt!");
                stopGame();
            }
        }

        void fill()
        {
            int card = 2;
            for (int i = 0; i < 52; i += 4)
            {
                // last row (ÁSZ)
                if (i >= 48 && i < 52)
                {
                    int r1 = random.Next(1, 10);
                    int r2 = random.Next(1, 10);
                    int r3 = random.Next(1, 10);
                    int r4 = random.Next(1, 10);

                    cards.Add(r1 > 5 ? 11 : 1);
                    cards.Add(r2 > 5 ? 11 : 1);
                    cards.Add(r3 > 5 ? 11 : 1);
                    cards.Add(r4 > 5 ? 11 : 1);
                }
                else
                {
                    cards.Add(card);
                    cards.Add(card);
                    cards.Add(card);
                    cards.Add(card);
                }

                // first row to before the last row
                if (card < 10)
                {
                    card++;
                }
            }
        }

        int getRandomCard()
        {
            int r = random.Next(0, cards.Count);
            int card = cards.ElementAt(r);
            cards.RemoveAt(r);
            return card;
        }

        /*** true: dealer, false: player */
        bool whoWon(Player player, Dealer dealer)
        {
            if (player.getCardSum() > dealer.getCardSum())
            {
                return false;
            }

            return true;
        }

        void stopGame()
        {
            isRunning = false;
        }

        void gameChoices(Player player, Dealer dealer)
        {
            Console.WriteLine(Program.GAME_PREFIX + "Játék lehetőségeid:");
            Console.WriteLine(Program.GAME_PREFIX + "\t- Hit (1)");
            Console.WriteLine(Program.GAME_PREFIX + "\t- Stand (2)");
            Console.WriteLine(Program.GAME_PREFIX + "\t- Double (3)");
            Console.WriteLine(Program.GAME_PREFIX + "\t- Surrender (4)");

            int choice = player.isStop() ? 2 : 0;

            if (!player.isStop())
            {
                int.TryParse(Console.ReadLine(), out choice);

                if (choice < 0 || choice > 3)
                {
                    Console.WriteLine(Program.GAME_PREFIX + "Játékszabályok megsértése.");
                    Console.WriteLine(Program.GAME_PREFIX + "Kizárva!");
                    stopGame();
                }
            }

            switch (choice)
            {
                case 1:
                    player.addCard(getRandomCard());
                    player.printStats();
                    break;
                case 2:
                    Console.WriteLine(Program.GAME_PREFIX + "Megálltál!");
                    Console.WriteLine(Program.GAME_PREFIX + "Várakozás az osztó döntésére...");
                    player.setIsStop(true);
                    bool dealerChoice = dealer.isContinue();

                    if (!dealerChoice)
                    {
                        Console.WriteLine(Program.GAME_PREFIX + "Az osztó is megállt!");
                        Console.WriteLine(Program.GAME_PREFIX + "Kártyák összehasonlítása...");
                        Thread.Sleep(1000);
                        Console.WriteLine(Program.GAME_PREFIX + "....");
                        Thread.Sleep(1000);
                        Console.WriteLine(Program.GAME_PREFIX + "...");
                        Thread.Sleep(1000);
                        Console.WriteLine(Program.GAME_PREFIX + ".");

                        if (player.getCardSum() == dealer.getCardSum())
                        {
                            throw new PushWin();
                        }

                        bool whoOneTheGame = whoWon(player, dealer);

                        Console.WriteLine(Program.GAME_PREFIX + "\n");
                        Console.WriteLine(Program.GAME_PREFIX + "A játék nyertese: " + (whoOneTheGame ? "Osztó" : "Te"));
                        if (whoOneTheGame)
                        {
                            Console.WriteLine(Program.GAME_PREFIX + "Osztó adatai:");
                            Console.WriteLine(Program.GAME_PREFIX + "Kártyái: " + dealer);
                            Console.WriteLine(Program.GAME_PREFIX + "Kártyák összege: " + dealer.getCardSum());
                        }

                        Console.WriteLine(Program.GAME_PREFIX + "\n");
                        stopGame();
                    }
                    else
                    {
                        dealer.addCard(getRandomCard());
                    }
                    break;
                case 3:
                    if (player.isDouble())
                    {
                        Console.WriteLine(Program.GAME_PREFIX + "Elutasítva! Egyszer már dupláztál!");
                    }
                    else
                    {
                        player.addCard(getRandomCard());
                        player.setDoubled(true);
                        player.printStats();
                    }
                    break;
                case 4:
                    Console.WriteLine(Program.GAME_PREFIX + "Köszönjük, hogy itt voltál!");
                    stopGame();
                    break;
            }
        }
    }
}
