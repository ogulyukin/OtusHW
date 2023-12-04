using Bullets;
using Character;
using Input;
using Level;
using UnityEngine;
using Zenject;

namespace Core
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private CharacterConfig character;
        [SerializeField] private InputManager inputManager;
        [SerializeField] private BulletSystemConfig bulletSystemConfig;
        [SerializeField] private LevelBackground levelBackground;
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().AsSingle().NonLazy();
            Container.Bind<CharacterConfig>().FromInstance(character).AsSingle();
            Container.Bind<BulletSystemConfig>().FromInstance(bulletSystemConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<BulletManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputManager>().FromInstance(inputManager).AsSingle();
            Container.Bind<CharacterFireControlComponent>().AsSingle();
            Container.BindInterfacesAndSelfTo<CharacterInputController>().AsSingle();
            Container.BindInterfacesTo<LevelBackground>().FromInstance(levelBackground).AsSingle();
            Debug.Log("SceneContainer work finished");
        }
    }
}
