using UnityEngine;
using Zenject;

namespace Flappy.Game
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BirdPresentation : MonoBehaviour
    {
        private BirdLogic _logic;

        [Inject]
        public void Construct(BirdLogic logic)
        {
            _logic = logic;
            
            var rigidbody = GetComponent<Rigidbody2D>();
            
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