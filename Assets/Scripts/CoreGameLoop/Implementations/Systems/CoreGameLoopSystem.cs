using Common.Interfaces;
using CoreGameLoop.Configs;
using CoreGameLoop.Interfaces;
using Screens.Interfaces;
using UnityEngine;

namespace CoreGameLoop.Implementations.Systems
{
    ///  <inheritdoc />
    public class CoreGameLoopSystem : ICoreGameLoopSystem
    {
        private IScreenSystem _screenSystem;
        private IGameContainer _gameContainer;

        private CoreGameLoopConfig _coreGameLoopConfig;

        private bool _isInitialized;

        /// <summary>
        /// Конструктор
        /// </summary>
        public CoreGameLoopSystem(IScreenSystem screenSystem,
            IGameContainer gameContainer,
            CoreGameLoopConfig coreGameLoopConfig)
        {
            _screenSystem = screenSystem;
            _gameContainer = gameContainer;

            _coreGameLoopConfig = coreGameLoopConfig;
        }

        ///  <inheritdoc />
        public void Initialize()
        {
            if (_isInitialized)
            {
                return;
            }

            _isInitialized = true;

            var _coreBackView = GameObject.Instantiate(_coreGameLoopConfig.CoreBackViewPrefab, _gameContainer.CoreContainer);
            _coreBackView.transform.SetAsFirstSibling();
        }

        ///  <inheritdoc />
        public void StartGameLoop()
        {
            var gameScreen = _screenSystem.ShowScreen<IGameScreen>();
        }

        ///  <inheritdoc />
        public void StopGameLoop()
        {
        }
    }
}