namespace MathGame.Models
{
    internal class Game
    {
        public DateTime Date { get; set; }
        public GameType GameType { get; set; }
        public int Score { get; set; }
        public Difficulties Difficulty { get; set; }
        public bool GameWon { get; set; }
    }

    internal enum GameType { Addition, Subtraction, Multiplication, Division };
    internal enum Difficulties { Easy, Medium, Hard };
}