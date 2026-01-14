using System;

namespace Flappy.Core
{
    public interface IGameState : IDisposable
    {
        void Start(); // При входе в состояние
        void Tick();  // Аналог Update
    }
}