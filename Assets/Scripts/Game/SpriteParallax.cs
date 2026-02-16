using UnityEngine;
using Zenject;
using Flappy.Core;

namespace Flappy.Game
{
    public class SpriteParallax : MonoBehaviour
    {
        [SerializeField] private float _scrollSpeed = 0.5f;
        [SerializeField] private Renderer _renderer;

        private SignalBus _signalBus;
        private bool _isMoving = false;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<GameStartSignal>(OnGameStart);
            _signalBus.Subscribe<BirdCrashedSignal>(OnBirdCrashed);
        }

        private void Update()
        {
            if (!_isMoving) return;

            float offset = Time.time * _scrollSpeed;

            _renderer.material.mainTextureOffset = new Vector2(offset, 0);
        }

        private void OnGameStart() => _isMoving = true;
        private void OnBirdCrashed() => _isMoving = false;

        private void OnDisable()
        {
            _signalBus.Unsubscribe<GameStartSignal>(OnGameStart);
            _signalBus.Unsubscribe<BirdCrashedSignal>(OnBirdCrashed);
        }
    }
}