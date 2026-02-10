using UnityEngine;
using Flappy.Core;

namespace Flappy.Game
{
    public class GameOverState : IGameState
    {
        public void Start()
        {
            Debug.Log("Game Over!");
            Time.timeScale = 0;
            // Показать UI перезапуска
        }

        public void Tick() { }
        public void Dispose() { }
    }
}