using UnityEngine;
using Flappy.Core;
using Zenject;

namespace Flappy.Game
{
    public class SpriteParallax : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;

        private GameConfig.WorldSettings _settings;

        [Inject]
        public void Construct(GameConfig.WorldSettings settings)
        {
            _settings = settings;
        }
        
        private void Update()
        {
            float offset = Time.time * _settings.ScrollSpeed;

            _renderer.material.mainTextureOffset = new Vector2(offset, 0);
        }
    }
}