using System;
using UnityEngine;
using Zenject;
using Flappy.Core;

namespace Flappy.Game
{
    public class BirdController : MonoBehaviour
    {
        [SerializeField] private Transform _transform;
        [SerializeField] private Rigidbody2D _rigidbody;

        private Vector2 _initialPosition;
        private SignalBus _signalBus;
        private InputHandler _inputHandler;
        
        private GameConfig.BirdSettings _settings;

        public event Action OnZoneGetScoreEntered;

        [Inject]
        public void Construct(SignalBus signalBus, InputHandler inputHandler, GameConfig.BirdSettings settings)
        {
            _signalBus = signalBus;
            _inputHandler = inputHandler;
            _transform = GetComponent<Transform>();
            _initialPosition = _transform.position;
            _settings = settings;
        }

        private void OnEnable()
        {
            _inputHandler.OnClicked += Jump;
            _rigidbody.gravityScale = _settings.GravityScale;
        }
        
        public void ResetPosition()
        {
            _transform.position = _initialPosition;
        }

        private void Jump()
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.velocity = Vector2.up * _settings.JumpForce;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.GetComponent<ZoneGameOver>() != null)
            {
                _signalBus.Fire<BirdCrashedSignal>();
            }
            
            if (other.gameObject.GetComponent<ZoneGetScore>() != null)
            {
                OnZoneGetScoreEntered?.Invoke();
            }
        }
        
        private void OnDisable()
        {
            _inputHandler.OnClicked -= Jump;
        }
    }
}