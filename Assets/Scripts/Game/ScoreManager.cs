using System;
using Flappy.Core;
using UnityEngine;
using Zenject;

namespace Flappy.Game
{
    public class ScoreManager : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;

        private int _currentScore;
        private int _recordScore;
        private int _totalScore;
        private int _totalGames = 1;
        private float _averageScore = 0;

        public ScoreManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            ResetCurrentScore();
            _signalBus.Subscribe<GetScoreSignal>(AddScore);
            _signalBus.Subscribe<GameStartSignal>(AddGameCount);
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<GetScoreSignal>(AddScore);
            _signalBus.Unsubscribe<GameStartSignal>(AddGameCount);
        }

        public void ResetCurrentScore()
        {
            _currentScore = 0;
            FireSignalScoreChanged();
        }

        private void AddGameCount()
        {
            _totalGames++;
        }
        
        private void AddScore()
        {
            _currentScore++;
            _totalScore++;

            if (_recordScore < _currentScore)
            {
                _recordScore = _currentScore;
            }

            _averageScore =  (float)_totalScore / (float)_totalGames;

            FireSignalScoreChanged();
        }
        
        private void FireSignalScoreChanged()
        { 
            _signalBus.Fire(new ScoreChangedSignal() { CurrentScore = _currentScore, RecordScore = _recordScore, TotalScore = _totalScore, AverageScore = _averageScore});
        }
    }
}