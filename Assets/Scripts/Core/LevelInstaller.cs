using Bullets;
using Character;
using Enemy.Manager;
using Input;
using Level;
using UI;
using UnityEngine;
using UniversalComponents;
using Zenject;

namespace Core
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private UnitConfig player;
        [SerializeField] private BulletSystemConfig bulletSystemConfig;
        [SerializeField] private LevelBackgroundConfig levelBackgroundConfig;
        [SerializeField] private EnemyManagerConfig enemyManagerConfig;
        [SerializeField] private MainMenuView mainMenuView;
        [SerializeField] private PauseButtonView pauseButtonView;
        [SerializeField] private GameLauncher gameLauncher;
        public override void InstallBindings()
        {
            CoreBinding();
            BulletSystemBinding();
            BackgroundBinding();
            BindPlayer();
            EnemyBindings();
            UIBinding();
        }

        private void CoreBinding()
        {
            Container.Bind<GameLauncher>().FromInstance(gameLauncher).AsSingle();
            Container.Bind<GameManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
        }

        private void BulletSystemBinding()
        {
            Container.Bind<BulletSystemConfig>().FromInstance(bulletSystemConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<BulletManager>().AsSingle();
        }

        private void BackgroundBinding()
        {
            Container.Bind<LevelBackgroundConfig>().FromInstance(levelBackgroundConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<LevelBackground>().AsSingle();
        }

        private void BindPlayer()
        {
            Container.Bind<UnitConfig>().FromInstance(player).AsSingle();
            Container.Bind<CharacterFireControlComponent>().AsSingle();
            Container.BindInterfacesTo<CharacterInputController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerStateObserver>().AsSingle();
        }

        private void EnemyBindings()
        {
            Container.Bind<EnemyManagerConfig>().FromInstance(enemyManagerConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyUpdater>().AsSingle();
        }

        private void UIBinding()
        {
            Container.BindInterfacesAndSelfTo<MainMenuView>().FromInstance(mainMenuView).AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseButtonView>().FromInstance(pauseButtonView).AsSingle();
            Container.BindInterfacesAndSelfTo<PauseButtonController>().AsSingle();
        }
    }
}
