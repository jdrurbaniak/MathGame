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

while (choiceInput == null || choiceInput == "" && exit == false)
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
void AdditionGame() { }
void SubtractionGame() { }
void MultipicationGame() { }
void DivisionGame() { }
int DifficultySelection()
{
    return (int)Difficulties.Easy;
}

enum Difficulties { Easy, Medium, Hard };