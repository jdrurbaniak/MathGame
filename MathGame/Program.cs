using MathGame;

string? playerName = null;
var helpers = new Helpers();

while (playerName == null || playerName == "")
{
    Helpers.ShowHeaderAndClearScreen();
    Console.WriteLine("Please enter your name: ");
    playerName = Console.ReadLine().Trim();
}

var date = DateTime.UtcNow;
Helpers.ShowHeaderAndClearScreen();
Console.WriteLine($"Hello {playerName}! It's currently {date.ToLocalTime().DayOfWeek}");

Menu menu = new Menu();
menu.ShowMenu(playerName);


