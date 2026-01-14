using UnityEngine;
using Flappy.Core;
using Zenject;

namespace Flappy.Game
{
    public class PlayingState : IGameState
    {
        private readonly SignalBus _signalBus;
        
        // Сюда можно инжектить спавнер труб, очки и т.д.
        public PlayingState(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Start()
        {
            Debug.Log("Game Started!");
            Time.timeScale = 1;
            // Здесь можно включить спавнер труб
        }

        public void Tick()
        {
            // Логика во время игры
        }

        public void Dispose()
        {
            // Очистка при выходе из состояния
        }
    }
}