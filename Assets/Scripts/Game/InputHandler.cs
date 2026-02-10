using Flappy.Core;
using Zenject;
using UnityEngine;

namespace Flappy.Game
{
    public class InputHandler : ITickable
    {
        private readonly SignalBus _signalBus;

        public InputHandler(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _signalBus.Fire<ClickSignal>();
            }
        }
    }
}