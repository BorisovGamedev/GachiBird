using System;
using Flappy.Core;
using Flappy.Game;
using UnityEngine;
using Zenject;

namespace Flappy.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private BirdController _birdPrefab;
        [SerializeField] private Transform _birdSpawnPoint;
        [SerializeField] private PipeView _pipePrefab;
        [SerializeField] private Transform _pipesContainer; 
        
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<ClickSignal>();
            Container.DeclareSignal<GetScoreSignal>();
            Container.DeclareSignal<BirdCrashedSignal>();
            Container.DeclareSignal<GameStartSignal>();
            Container.DeclareSignal<ScoreChangedSignal>();

            Container.BindInterfacesTo<InputHandler>().AsSingle();

            Container.Bind<StartState>().AsSingle();
            Container.Bind<PlayingState>().AsSingle();
            Container.Bind<GameOverState>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<ScoreManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PipeSpawner>().AsSingle();

            Container.Bind<BirdController>()
                .FromComponentInNewPrefab(_birdPrefab)
                .UnderTransform(_birdSpawnPoint)
                .AsSingle()
                .NonLazy();
            
            Container.BindMemoryPool<PipeView, PipeView.Pool>()
                .WithInitialSize(5)
                .FromComponentInNewPrefab(_pipePrefab)
                .UnderTransform(_pipesContainer);
        }
    }
}
