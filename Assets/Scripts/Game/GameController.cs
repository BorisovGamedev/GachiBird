using System;
using Zenject;
using Flappy.Core;

namespace Flappy.Game
{
    public class GameController : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly StartState _startState;
        private readonly PlayingState _playingState;
        private readonly GameOverState _gameOverState;
        private readonly InputHandler _inputHandler;
        
        private IGameState _currentState;

        public GameController(SignalBus signalBus, StartState startState, PlayingState playing, GameOverState gameOver, InputHandler inputHandler)
        {
            _signalBus = signalBus;
            _startState = startState;
            _playingState = playing;
            _gameOverState = gameOver;
            _inputHandler = inputHandler;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<BirdCrashedSignal>(OnBirdCrashed);
            _inputHandler.OnClicked += StartGame;
            
            ChangeState(_startState);
        }

        private void StartGame()
        {
            if (_currentState == _playingState)
            {
                return;
            }
            
            if (_currentState == _gameOverState)
            {
                ChangeState(_startState);
                _signalBus.Fire<GameStartSignal>();
                return;
            }

            if (_currentState == _startState)
            {
                ChangeState(_playingState);
            }
        }

        private void OnBirdCrashed()
        {
            ChangeState(_gameOverState);
        }

        private void ChangeState(IGameState newState)
        {
            _currentState?.Dispose();
            _currentState = newState;
            _currentState.Start();
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<BirdCrashedSignal>(OnBirdCrashed);
            _inputHandler.OnClicked -= StartGame;
        }
    }
}