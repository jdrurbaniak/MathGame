using MathGame.Models;

namespace MathGame
{
    internal class GameEngine
    {
        internal List<Game> gamesList = new();
        Difficulties DifficultySelection()
        {
            Helpers.ShowHeaderAndClearScreen();
            int choiceDisplayNumber = 1;
            Console.WriteLine(@$"Choose your difficulty:
{choiceDisplayNumber++} - Easy
{choiceDisplayNumber++} - Medium
{choiceDisplayNumber++} - Hard");
            string? choiceInput = null;
            while (choiceInput == null)
            {
                choiceInput = Console.ReadLine().Trim().ToLower();
                switch (choiceInput)
                {
                    case "1":
                        return Difficulties.Easy;
                    case "2":
                        return Difficulties.Medium;
                    case "3":
                        return Difficulties.Hard;
                    default:
                        if (choiceInput == "")
                            Console.Write("You didn't select an option! ");
                        else
                            Console.Write($"{choiceInput} is not a valid option! ");
                        choiceInput = null;
                        Helpers.PausePrompt();
                        Helpers.ShowHeaderAndClearScreen();
                        continue;
                }
            }
            return Difficulties.Medium; // Default value, all code paths must return a value
        }

        internal void StartGame(GameType gameType, string playerName)
        {
            var difficulty = DifficultySelection();
            const int startingScore = 100;
            const int rightAnswerReward = 5;
            const int wrongAnswerPenalty = 10;
            bool negativeNumbersAllowed = false;
            int requiredScoreToWin = 225;
            int maximumValue = 10;
            int playerScore = startingScore;

            switch (difficulty)
            {
                case Difficulties.Easy:
                    requiredScoreToWin = 150;
                    negativeNumbersAllowed = false;
                    maximumValue = 10;
                    break;
                case Difficulties.Hard:
                    requiredScoreToWin = 225;
                    negativeNumbersAllowed = true;
                    maximumValue = (gameType == GameType.Division) ? 50 : 1000;
                    break;
                default:
                    requiredScoreToWin = 200;
                    negativeNumbersAllowed = false;
                    maximumValue = (gameType == GameType.Division) ? 25 : 100;
                    break;
            }

            Helpers.ShowHeaderAndClearScreen();
            Console.WriteLine($"Get {requiredScoreToWin} point to win!");
            bool continuePlaying = true;

            while (continuePlaying == true)
            {
                Console.WriteLine($"{playerName}'s score: {playerScore}");
                Random random = new Random();
                int firstNumber = 0, secondNumber = 0;
                secondNumber = random.Next(1, maximumValue + 1);

                if (gameType == GameType.Division)
                {
                    firstNumber = random.Next(1, (maximumValue * 2) / secondNumber + 1) * secondNumber;
                }
                else
                {

                    firstNumber = random.Next(1, maximumValue + 1);
                }

                int numberFromPlayer = 0;
                string firstNumberFormatted = firstNumber.ToString();
                string secondNumberFormatted = secondNumber.ToString();



                if (negativeNumbersAllowed)
                {
                    if (random.Next(0, 2) == 1)
                    {
                        firstNumber = -firstNumber;
                        firstNumberFormatted = $"({firstNumber})";
                    }
                    if (random.Next(0, 2) == 1)
                    {
                        secondNumber = -secondNumber;
                        secondNumberFormatted = $"({secondNumber})";
                    }
                }

                string? userInput = null;

                string[] signs = ["+", "-", "*", "/"];

                while (userInput == null || userInput == "")
                {
                    Console.Write($"{firstNumberFormatted} {signs[(int)gameType]} {secondNumberFormatted} = ");
                    userInput = Console.ReadLine().Trim();

                    if (int.TryParse(userInput, out numberFromPlayer) == false)
                    {
                        userInput = null;
                        Helpers.ShowHeaderAndClearScreen();
                        Console.WriteLine("Please enter a number.");
                    }
                }

                bool answerCorrect = false;

                switch (gameType)
                {
                    case GameType.Addition:
                        answerCorrect = numberFromPlayer == firstNumber + secondNumber;
                        break;
                    case GameType.Subtraction:
                        answerCorrect = numberFromPlayer == firstNumber - secondNumber;
                        break;
                    case GameType.Multiplication:
                        answerCorrect = numberFromPlayer == firstNumber * secondNumber;
                        break;
                    case GameType.Division:
                        answerCorrect = numberFromPlayer == firstNumber / secondNumber;
                        break;
                }

                if (answerCorrect == true)
                {
                    playerScore += rightAnswerReward;
                    Helpers.ShowHeaderAndClearScreen();
                    Console.WriteLine($"Correct answer! +{rightAnswerReward} points");
                }
                else
                {
                    playerScore -= wrongAnswerPenalty;
                    Helpers.ShowHeaderAndClearScreen();
                    Console.WriteLine($"Wrong answer! -{wrongAnswerPenalty} points");
                }

                if (playerScore >= requiredScoreToWin)
                {
                    continuePlaying = false;
                    gamesList.Add(new Game {
                        Date = DateTime.Now,
                        GameType = gameType,
                        Score = playerScore,
                        Difficulty = difficulty,
                        GameWon = true,
                    });
                    Console.WriteLine($"You've reached {playerScore} points.");
                    Console.WriteLine($"Congratulations {playerName}! You've won!");
                    Helpers.PausePrompt();
                    Helpers.ShowHeaderAndClearScreen();
                }
                else if (playerScore <= 0)
                {
                    continuePlaying = false;
                    gamesList.Add(new Game
                    {
                        Date = DateTime.Now,
                        GameType = gameType,
                        Score = playerScore,
                        Difficulty = difficulty,
                        GameWon = false,
                    });
                    Console.WriteLine("You have 0 points. You lose.");
                    Helpers.PausePrompt();
                    Helpers.ShowHeaderAndClearScreen();
                }
            }
        }
    }
}
