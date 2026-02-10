using System;
using Zenject;
using Flappy.Core;
using UnityEngine;

namespace Flappy.Game
{
    public class GameController : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly StartState _startState;
        private readonly PlayingState _playingState;
        private readonly GameOverState _gameOverState;
        
        private IGameState _currentState;

        public GameController(SignalBus signalBus, StartState startState, PlayingState playing, GameOverState gameOver)
        {
            _signalBus = signalBus;
            _startState = startState;
            _playingState = playing;
            _gameOverState = gameOver;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ClickSignal>(StartGame);
            _signalBus.Subscribe<BirdCrashedSignal>(OnBirdCrashed);
            
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
            _signalBus.Unsubscribe<ClickSignal>(StartGame);
            _signalBus.Unsubscribe<BirdCrashedSignal>(OnBirdCrashed);
        }
    }
}