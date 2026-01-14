using Zenject;
using Flappy.Core;
using Flappy.Game;
using Game;
using UnityEngine;

namespace Flappy.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private BirdController _birdPrefab;
        [SerializeField] private Transform _startPoint;

        public override void InstallBindings()
        {
            // 1. Установка SignalBus
            SignalBusInstaller.Install(Container);

            // 2. Объявление сигналов (Обязательно!)
            Container.DeclareSignal<JumpInputSignal>();
            Container.DeclareSignal<BirdCrashedSignal>();
            Container.DeclareSignal<GameStartSignal>();

            // 3. Биндинг InputHandler
            // BindInterfacesTo означает, что он привяжется к ITickable (будет вызываться Tick)
            Container.BindInterfacesTo<InputHandler>().AsSingle();

            // 4. Биндинг Состояний (Как обычные классы)
            Container.Bind<PlayingState>().AsSingle();
            Container.Bind<GameOverState>().AsSingle();

            // 5. Биндинг GameController (Он IInitializable, поэтому запустится сам)
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();

            // 6. Биндинг Птицы (Создаем её на сцене)
            Container.Bind<BirdController>()
                .FromComponentInNewPrefab(_birdPrefab)
                .UnderTransform(_startPoint)
                .AsSingle()
                .NonLazy(); // Создать сразу при старте
        }
    }
}