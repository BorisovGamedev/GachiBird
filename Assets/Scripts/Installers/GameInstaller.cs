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
        [SerializeField] private PipeView _pipePrefab;
        [SerializeField] private Transform _birdSpawnPoint;
        
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
            
            Container.BindFactory<PipeView, PipeView.Factory>()
                .FromComponentInNewPrefab(_pipePrefab);//трубы спавнятся поверх Canvas, поэтому их не видно
        }
    }
}
