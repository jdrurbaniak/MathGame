const string mathGameLogo = $"\t---Math Game---\n";
string? playerName = null;

MainMenu();
void ShowHeaderAndClearScreen()
{
    Console.Clear();
    Console.WriteLine(mathGameLogo);
}
void MainMenu()
{
    while (playerName == null || playerName == "")
    {
        ShowHeaderAndClearScreen();
        Console.WriteLine("Please enter your name: ");
        playerName = Console.ReadLine().Trim();
    }

    var date = DateTime.UtcNow;
    ShowHeaderAndClearScreen();
    Console.WriteLine($"Hello {playerName}! It's currently {date.ToLocalTime()}");

    string? choiceInput = null;
    bool exit = false;

    Console.Write("What game do you want to play? ");

    while (exit == false)
    {
        int choiceDisplayNumber = 1;
        Console.WriteLine($@"Choose from these options:
{choiceDisplayNumber++} - Addition
{choiceDisplayNumber++} - Subtraction
{choiceDisplayNumber++} - Multiplication
{choiceDisplayNumber++} - Division
{choiceDisplayNumber++} - Quit");
        choiceInput = Console.ReadLine().Trim().ToLower();
        Console.Clear();
        switch (choiceInput)
        {
            case "1":
                Game(GameType.Addition);
                break;
            case "2":
                Game(GameType.Subtraction);
                break;
            case "3":
                Game(GameType.Multiplication);
                break;
            case "4":
                Game(GameType.Division);
                break;
            case "5":
            case "q":
            case "exit":
                exit = true;
                break;
            default:
                ShowHeaderAndClearScreen();
                if (choiceInput == "")
                    Console.Write("You didn't select an option! ");
                else
                    Console.Write($"{choiceInput} is not a valid option! ");
                choiceInput = null;
                continue;
        }
    }
}

void Game(GameType gameType)
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
            maximumValue = 1000;
            break;
        default:
            requiredScoreToWin = 200;
            negativeNumbersAllowed = false;
            maximumValue = 100;
            break;
    }

    ShowHeaderAndClearScreen();
    Console.WriteLine($"Get {requiredScoreToWin} point to win!");
    bool continuePlaying = true;

    while (continuePlaying == true)
    {
        Console.WriteLine($"{playerName}'s score: {playerScore}");
        Random random = new Random();
        int firstNumber = random.Next(1, maximumValue + 1);
        int secondNumber = random.Next(1, maximumValue + 1);
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
            userInput = TruncateAfterSubstring(userInput, ".");
            userInput = TruncateAfterSubstring(userInput, ",");

            if (int.TryParse(userInput, out numberFromPlayer) == false)
            {
                userInput = null;
                ShowHeaderAndClearScreen();
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
            ShowHeaderAndClearScreen();
            Console.WriteLine($"Correct answer! +{rightAnswerReward} points");
        }
        else
        {
            playerScore -= wrongAnswerPenalty;
            ShowHeaderAndClearScreen();
            Console.WriteLine($"Wrong answer! -{wrongAnswerPenalty} points");
        }

        if (playerScore > requiredScoreToWin)
        {
            continuePlaying = false;
            Console.WriteLine($"You've reached {requiredScoreToWin} points.");
            Console.WriteLine($"Congratulations {playerName}! You've won!");
            PausePrompt();
            ShowHeaderAndClearScreen();
        }
        else if (playerScore <= 0)
        {
            continuePlaying = false;
            Console.WriteLine("You have 0 points. You lose.");
            PausePrompt();
            ShowHeaderAndClearScreen();
        }
    }
}

Difficulties DifficultySelection()
{
    ShowHeaderAndClearScreen();
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
                PausePrompt();
                ShowHeaderAndClearScreen();
                continue;
        }
    }
    return Difficulties.Medium; // Default value, all code paths must return a value
}

string TruncateAfterSubstring(string text, string substring)
{
    if(text.Contains(substring))
    {
        text = text.Remove(text.IndexOf(substring));
    }
    return text;
}
void PausePrompt()
{
    Console.WriteLine("Press any button to continue");
    Console.ReadKey();
}
enum Difficulties { Easy, Medium, Hard };
enum GameType { Addition, Subtraction, Multiplication, Division };