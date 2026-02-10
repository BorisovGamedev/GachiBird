using UnityEngine;
using Zenject;
using Flappy.Core;

namespace Flappy.Game
{
    public class BirdController : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _jumpForce = 1f;

        private Vector2 _initialPosition;
        private SignalBus _signalBus;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _rectTransform = GetComponent<RectTransform>();
            _initialPosition = _rectTransform.anchoredPosition;
        }

        public void ResetPosition()
        {
            _rectTransform.anchoredPosition = _initialPosition;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<ClickSignal>(Jump);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<ClickSignal>(Jump);
        }

        private void Jump()
        {
            _rigidbody.velocity = Vector2.up * _jumpForce;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<ZoneGameOver>() != null)
            {
                _signalBus.Fire<BirdCrashedSignal>();
            }
            
            if (other.gameObject.GetComponent<ZoneGetScore>() != null)
            {
                _signalBus.Fire<GetScoreSignal>();
            }
        }
    }
}