using System;
using Flappy.Core;
using Zenject;

namespace Flappy.Game
{
    public class ScoreManager : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;

        private int _currentScore;
        private int _recordScore;
        private int _totalScore;

        public ScoreManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            ResetCurrentScore();
            _signalBus.Subscribe<GetScoreSignal>(AddScore);
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<GetScoreSignal>(AddScore);
        }

        public void ResetCurrentScore()
        {
            _currentScore = 0;
            FireSignalScoreChanged();
        }
        
        private void AddScore()
        {
            _currentScore++;
            _totalScore++;

            if (_recordScore < _currentScore)
            {
                _recordScore = _currentScore;
            }

            FireSignalScoreChanged();
        }
        
        private void FireSignalScoreChanged()
        { 
            _signalBus.Fire(new ScoreChangedSignal() { CurrentScore = _currentScore, RecordScore = _recordScore, TotalScore = _totalScore });
        }
    }
}