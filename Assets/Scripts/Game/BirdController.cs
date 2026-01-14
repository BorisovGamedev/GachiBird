using UnityEngine;
using Zenject;
using Flappy.Core;

namespace Game
{
    public class BirdController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _jumpForce = 5f;

        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            // Подписываемся на сигнал прыжка
            _signalBus.Subscribe<JumpInputSignal>(Jump);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<JumpInputSignal>(Jump);
        }

        private void Jump()
        {
            _rigidbody.velocity = Vector2.up * _jumpForce;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            // Птица не вызывает GameOverManager. Она просто сообщает факт столкновения.
            _signalBus.Fire<BirdCrashedSignal>();
        }
    }
}