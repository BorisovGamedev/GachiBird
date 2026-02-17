using System;
using UnityEngine;
using Zenject;
using Flappy.Core;

namespace Flappy.Game
{
    public class BirdLogic : IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly InputHandler _inputHandler;
        private readonly GameConfig.BirdSettings _settings;

        private Rigidbody2D _rigidbody;
        private Transform _transform;
        private Vector2 _initialPosition;

        public event Action OnZoneGetScoreEntered;

        public BirdLogic(SignalBus signalBus, InputHandler inputHandler, GameConfig.BirdSettings settings)
        {
            _signalBus = signalBus;
            _inputHandler = inputHandler;
            _settings = settings;
        }

        public void Initialize(Rigidbody2D rigidbody, Transform transform)
        {
            _rigidbody = rigidbody;
            _transform = transform;
            _initialPosition = _transform.position;

            _rigidbody.gravityScale = _settings.GravityScale;

            _inputHandler.OnClicked += Jump;
        }

        public void ResetPosition()
        {
            _transform.position = _initialPosition;
            _rigidbody.velocity = Vector2.zero;
        }

        private void Jump()
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.velocity = Vector2.up * _settings.JumpForce;
        }

        public void HandleTriggerEnter(Collider2D other)
        {
            if (other.TryGetComponent(out ZoneGameOver gameOverZone))
            {
                _signalBus.Fire<BirdCrashedSignal>();
            }

            if (other.TryGetComponent(out ZoneGetScore scoreZone))
            {
                OnZoneGetScoreEntered?.Invoke();
            }
        }

        public void Dispose()
        {
            _inputHandler.OnClicked -= Jump;
        }
    }
}