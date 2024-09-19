using MathGame.Models;

namespace MathGame
{
    internal class Menu
    {
        GameEngine engine = new();
        internal void ShowMenu(string playerName)
        {
            string? choiceInput = null;
            bool exit = false;

            Console.Write("What game do you want to play? ");

            while (exit == false)
            {
                Console.WriteLine($@"Choose from these options:
1 - Addition game
2 - Subtraction game
3 - Multiplication game
4 - Division game
5 - Show records of past games
6 - Quit");
                choiceInput = Console.ReadLine().Trim().ToLower();
                Console.Clear();
                switch (choiceInput)
                {
                    case "1":
                        engine.StartGame(GameType.Addition, playerName);
                        break;
                    case "2":
                        engine.StartGame(GameType.Subtraction, playerName);
                        break;
                    case "3":
                        engine.StartGame(GameType.Multiplication, playerName);
                        break;
                    case "4":
                        engine.StartGame(GameType.Division, playerName);
                        break;
                    case "5":
                        Helpers.ShowHeaderAndClearScreen();
                        Helpers.ShowGameRecords(engine.gamesList);
                        Helpers.PausePrompt();
                        Helpers.ShowHeaderAndClearScreen();
                        break;
                    case "6":
                    case "q":
                    case "exit":
                        exit = true;
                        break;
                    default:
                        Helpers.ShowHeaderAndClearScreen();
                        if (choiceInput == "")
                            Console.Write("You didn't select an option! ");
                        else
                            Console.Write($"{choiceInput} is not a valid option! ");
                        choiceInput = null;
                        continue;
                }
            }
        }
    }
}
