using System;

namespace Flappy.Game
{
    public class ScoreProvider
    {
        public int CurrentScore { get; private set; } = 0;
        public int RecordScore { get; private set; } = 0;
        public int TotalScore { get; private set; } = 0;

        public event Action ValuesChanged;

        public void AddScore()
        {
            CurrentScore++;
            TotalScore++;

            if (CurrentScore > RecordScore)
            {
                RecordScore = CurrentScore;
            }
            
            ValuesChanged?.Invoke();
        }

        public void ResetCurrentScore()
        {
            CurrentScore = 0;
            ValuesChanged?.Invoke();
        }
    }
}