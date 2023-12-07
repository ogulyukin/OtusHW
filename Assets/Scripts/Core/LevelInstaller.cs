using Bullets;
using Character;
using Enemy;
using Input;
using Level;
using UI;
using UnityEngine;
using UnityEngine.Serialization;
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
        [FormerlySerializedAs("mainMenuHandler")] [SerializeField] private MainMenuView mainMenuView;
        [FormerlySerializedAs("pauseButtonHandler")] [SerializeField] private PauseButtonView pauseButtonView;
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
            Container.BindInterfacesAndSelfTo<MainMenuView>().FromInstance(mainMenuView).AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuController>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseButtonView>().FromInstance(pauseButtonView).AsSingle();
            Container.BindInterfacesAndSelfTo<PauseButtonController>().AsSingle();
        }
    }
}
