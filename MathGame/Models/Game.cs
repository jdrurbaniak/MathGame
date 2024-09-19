namespace MathGame.Models
{
    internal class Game
    {
        public DateTime Date { get; set; }
        public GameEngine.GameType GameType { get; set; }
        public int Score { get; set; }
        public GameEngine.Difficulties Difficulty { get; set; }
        public bool GameWon { get; set; }
    }
}
