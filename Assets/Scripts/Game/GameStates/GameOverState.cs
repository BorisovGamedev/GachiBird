using UnityEngine;
using Flappy.Core;

namespace Flappy.Game
{
    public class GameOverState : IGameState
    {
        private readonly GameOverWindow _window;
        
        public GameOverState(GameOverWindow window)
        {
            _window = window;
        }
        
        public void Start()
        {
            Time.timeScale = 0;
            _window.Show();
        }

        public void Tick() { }

        public void Dispose()
        {
            _window.Hide();
        }
    }
}