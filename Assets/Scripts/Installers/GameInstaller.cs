using Flappy.Core;
using Flappy.Game;
using UnityEngine;
using Zenject;

namespace Flappy.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig _gameConfig;
        
        [SerializeField] private BirdPresentation _birdPrefab;
        [SerializeField] private Transform _birdSpawnPoint;
        [SerializeField] private PipePresentation _pipePrefab;
        [SerializeField] private Transform _pipesContainer;
        
        [SerializeField] private StartWindow _startWindow;
        [SerializeField] private GameOverWindow _gameOverWindow;
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.BindInstance(_gameConfig.Bird);
            Container.BindInstance(_gameConfig.Pipes);
            Container.BindInstance(_gameConfig.World);

            Container.DeclareSignal<BirdCrashedSignal>();
            Container.DeclareSignal<GameStartSignal>();

            Container.BindInterfacesAndSelfTo<InputHandler>().AsSingle();

            Container.Bind<StickyAdActivator>().AsSingle().NonLazy();
            
            Container.Bind<StartState>().AsSingle();
            Container.Bind<PlayingState>().AsSingle();
            Container.Bind<GameOverState>().AsSingle();
            Container.Bind<ScoreView>().AsSingle();
            Container.Bind<ScoreProvider>().AsSingle();
            Container.Bind<BirdLogic>().AsSingle();
            
            Container.Bind<StartWindow>().FromInstance(_startWindow).AsSingle();
            Container.Bind<GameOverWindow>().FromInstance(_gameOverWindow).AsSingle();
            
            Container.BindInterfacesAndSelfTo<ScoreManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PipeSpawner>().AsSingle();

            Container.Bind<BirdPresentation>()
                .FromComponentInNewPrefab(_birdPrefab)
                .UnderTransform(_birdSpawnPoint)
                .AsSingle()
                .NonLazy();
            
            Container.BindMemoryPool<PipePresentation, PipePresentation.Pool>()
                .WithInitialSize(_gameConfig.Pipes.PoolSize)
                .FromComponentInNewPrefab(_pipePrefab)
                .UnderTransform(_pipesContainer);
        }
    }
}
