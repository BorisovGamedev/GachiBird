using System;

namespace Flappy.Core
{
    public interface IGameState : IDisposable
    {
        void Start();
        void Tick();
    }
}