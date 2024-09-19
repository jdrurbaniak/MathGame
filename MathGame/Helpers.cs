using MathGame.Models;

namespace MathGame
{
    internal class Helpers
    {
        const string mathGameLogo = $"\t---Math Game---\n";
        internal static void ShowHeaderAndClearScreen()
        {
            Console.Clear();
            Console.WriteLine(mathGameLogo);
        }
        internal static void ShowGameRecords(List<Game> gamesList)
        {
            if (gamesList.Count == 0)
            {
                Console.WriteLine("You have no past games. ");
            }
            else
            {
                foreach (var game in gamesList)
                {
                    Console.WriteLine($"{game.Date} - {game.GameType}:".PadRight(38) + $"Game {(game.GameWon ? "won" : "lost")} with {game.Score} points, on {game.Difficulty} difficulty");
                }
            }
        }

        internal static void PausePrompt()
        {
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
        }
    }
}
