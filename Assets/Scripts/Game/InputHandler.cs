using System;
using Zenject;
using UnityEngine;

namespace Flappy.Game
{
    public class InputHandler : ITickable
    {
        public event Action OnClicked;

        public InputHandler()
        {

        }

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnClicked?.Invoke();
            }
        }
    }
}