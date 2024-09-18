const string mathGameLogo = $"\t---Math Game---\n";
string? playerName = null;

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
            AdditionGame();
            break;
        case "2":
            SubtractionGame();
            break;
        case "3":
            MultipicationGame();
            break;
        case "4":
            DivisionGame();
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

void ShowHeaderAndClearScreen()
{
    Console.Clear();
    Console.WriteLine(mathGameLogo);
}
void AdditionGame()
{
    int difficulty = DifficultySelection();
    const int startingScore = 100;
    const int rightAnswerReward = 5;
    const int wrongAnswerPenalty = 10;
    bool negativeNumbersAllowed = false;
    int requiredScoreToWin = 225;
    int maximumValue = 10;
    int playerScore = startingScore;

    switch (difficulty)
    {
        case (int)Difficulties.Easy:
            requiredScoreToWin = 150;
            negativeNumbersAllowed = false;
            maximumValue = 10;
            break;
        case (int)Difficulties.Hard:
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
        if (negativeNumbersAllowed)
        {
            if (random.Next(0, 1) == 1)
                firstNumber = -firstNumber;
            if (random.Next(0, 1) == 1)
                secondNumber = -secondNumber;
        }
        string? userInput = null;
        while (userInput == null || userInput == "")
        {
            Console.Write($"{firstNumber} + {secondNumber} = ");
            userInput = Console.ReadLine().Trim();
            if (Int32.TryParse(userInput, out numberFromPlayer) == false)
            {
                userInput = null;
                ShowHeaderAndClearScreen();
                Console.WriteLine("Please enter a number.");
            }
        }
        if (numberFromPlayer == firstNumber + secondNumber)
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

void SubtractionGame() { }
void MultipicationGame() { }
void DivisionGame() { }

int DifficultySelection()
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
                return (int)Difficulties.Easy;
            case "2":
                return (int)Difficulties.Medium;
            case "3":
                return (int)Difficulties.Hard;
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
    return -1;
}

void PausePrompt()
{
    Console.WriteLine("Press any button to continue");
    Console.ReadKey();
}
enum Difficulties { Easy, Medium, Hard };