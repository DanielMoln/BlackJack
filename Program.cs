using BlackJack.game;

namespace BlackJack
{
    internal class Program
    {
        public static string GAME_PREFIX = "BlackJack | ";

        static void Main(string[] args)
        {
            Game game = new Game();
            game.run();
        }

    }
}