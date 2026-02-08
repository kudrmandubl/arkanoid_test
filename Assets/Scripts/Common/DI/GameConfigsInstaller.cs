using Balls.Configs;
using Buffs.Configs;
using Cameras.Configs;
using Common.Configs;
using CoreGameLoop.Configs;
using GameField.Configs;
using MainMenu.Configs;
using Racket.Configs;
using Screens.Configs;
using UnityEngine;
using Zenject;

namespace Common.DI
{
    /// <summary>
    /// DI инстоллер для конфигов игры
    /// </summary>
    [CreateAssetMenu(fileName = "GameConfigsInstaller", menuName = "Installers/GameConfigsInstaller")]
    public class GameConfigsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private ScreensConfig _screensConfig;
        [SerializeField] private TweenAnimationsConfig _tweenAnimationsConfig;
        [SerializeField] private CoreGameLoopConfig _coreGameLoopConfig;
        [SerializeField] private MainMenuConfig _mainMenuConfig;
        [SerializeField] private GameFieldConfig _gameFieldConfig;
        [SerializeField] private RacketConfig _racketConfig;
        [SerializeField] private BallsConfig _ballsConfig;
        [SerializeField] private CameraConfig _cameraConfig;
        [SerializeField] private BuffsConfig _buffsConfig;

        /// <summary>
        /// Установить зависимости
        /// </summary>
        public override void InstallBindings()
        {
            Container.BindInstance<ScreensConfig>(_screensConfig);
            Container.BindInstance<TweenAnimationsConfig>(_tweenAnimationsConfig);
            Container.BindInstance<CoreGameLoopConfig>(_coreGameLoopConfig);
            Container.BindInstance<MainMenuConfig>(_mainMenuConfig);
            Container.BindInstance<GameFieldConfig>(_gameFieldConfig);
            Container.BindInstance<RacketConfig>(_racketConfig);
            Container.BindInstance<BallsConfig>(_ballsConfig);
            Container.BindInstance<CameraConfig>(_cameraConfig);
            Container.BindInstance<BuffsConfig>(_buffsConfig);
        }
    }
}