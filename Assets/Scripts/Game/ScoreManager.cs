using System;
using Zenject;
using Flappy.Core;

namespace Flappy.Game
{
    public class ScoreManager : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        
        private ScoreProvider _scoreProvider;
        private BirdLogic _birdLogic;
        private ScoreView _scoreView;

        public ScoreManager(SignalBus signalBus,  ScoreProvider scoreProvider, BirdLogic birdLogic)
        {
            _signalBus = signalBus;
            _scoreProvider = scoreProvider;
            _birdLogic =  birdLogic;
        }

        public void Initialize()
        {
            ResetCurrentScore();
            _signalBus.Subscribe<GameStartSignal>(ResetCurrentScore);
            _birdLogic.OnZoneGetScoreEntered += AddScore;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameStartSignal>(ResetCurrentScore);
            _birdLogic.OnZoneGetScoreEntered -= AddScore;
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