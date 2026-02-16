using System;
using Zenject;
using Flappy.Core;

namespace Flappy.Game
{
    public class ScoreManager : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        
        private ScoreProvider _scoreProvider;
        private BirdController _birdController;
        private ScoreView _scoreView;

        public ScoreManager(SignalBus signalBus,  ScoreProvider scoreProvider, BirdController birdController)
        {
            _signalBus = signalBus;
            _scoreProvider = scoreProvider;
            _birdController =  birdController;
        }

        public void Initialize()
        {
            ResetCurrentScore();
            _signalBus.Subscribe<GameStartSignal>(ResetCurrentScore);
            _birdController.OnZoneGetScoreEntered += AddScore;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameStartSignal>(ResetCurrentScore);
            _birdController.OnZoneGetScoreEntered -= AddScore;
        }

        public void ResetCurrentScore()
        {
            _scoreProvider.ResetCurrentScore();
        }

        private void AddScore()
        {
            _scoreProvider.AddScore();
        }
    }
}