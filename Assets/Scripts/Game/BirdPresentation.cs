using System;
using UnityEngine;
using Zenject;

namespace Flappy.Game
{
    public class BirdPresentation : MonoBehaviour
    {
        private BirdLogic _logic;

        [Inject]
        public void Construct(BirdLogic logic)
        {
            _logic = logic;
            
            if (!TryGetComponent(out Rigidbody2D rigidbody))
            {
                Debug.LogError("Rigidbody2D not found on Bird!");
            }
            
            _logic.Initialize(rigidbody, transform);
        }

        public void ResetPosition()
        {
            _logic.ResetPosition();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            _logic.HandleTriggerEnter(other);
        }

        private void OnDestroy()
        {
            _logic.Dispose();
        }
    }
}