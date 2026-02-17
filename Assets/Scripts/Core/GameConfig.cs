using UnityEngine;

namespace Flappy.Core
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Flappy/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Настройки Птицы")]
        public BirdSettings Bird;

        [Header("Настройки Труб")]
        public PipeSettings Pipes;

        [Header("Настройки Мира")]
        public WorldSettings World;
        
        [System.Serializable]
        public class BirdSettings
        {
            public float JumpForce = 8f;
            public float GravityScale = 10f;
        }

        [System.Serializable]
        public class PipeSettings
        {
            public float Speed = 7f;
            public float SpawnInterval = 2f;
            public float MinY = -4f;
            public float MaxY = 4f;
            public float OffsetX = 30f;
            public int PoolSize = 5;
        }

        [System.Serializable]
        public class WorldSettings
        {
            public float ScrollSpeed = 0.33f;
        }
    }
}