using Bullets;
using Character;
using Enemy;
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
        [SerializeField] private LevelBackground levelBackground;
        [SerializeField] private EnemyManager enemyManager;
        [SerializeField] private MainMenuHandler mainMenuHandler;
        [SerializeField] private PauseButtonHandler pauseButtonHandler;
        [SerializeField] private GameLauncher gameLauncher;
        public override void InstallBindings()
        {
            Container.Bind<GameLauncher>().FromInstance(gameLauncher).AsSingle();
            Container.Bind<GameManager>().AsSingle();
            Container.Bind<BulletSystemConfig>().FromInstance(bulletSystemConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<BulletManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
            Container.BindInterfacesTo<LevelBackground>().FromInstance(levelBackground).AsSingle();
            BindPlayer();
            EnemyBindings();
            UIBinding();
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
            Container.BindInterfacesAndSelfTo<EnemyManager>().FromInstance(enemyManager).AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyUpdater>().AsSingle();
        }

        private void UIBinding()
        {
            Container.BindInterfacesAndSelfTo<MainMenuHandler>().FromInstance(mainMenuHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseButtonHandler>().FromInstance(pauseButtonHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<PauseButtonController>().AsSingle();
        }
    }
}
