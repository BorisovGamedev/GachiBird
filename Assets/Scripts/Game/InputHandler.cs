using UnityEngine;
using Zenject;
using Flappy.Core;

namespace Flappy.Game
{
    public class InputHandler : ITickable // Zenject интерфейс для Update
    {
        private readonly SignalBus _signalBus;

        public InputHandler(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                _signalBus.Fire<JumpInputSignal>();
            }
        }
    }
}