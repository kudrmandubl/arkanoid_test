using System;
using Common.Interfaces;
using CoreGameLoop.Configs;
using CoreGameLoop.Interfaces;
using GameField.Interfaces;
using Screens.Interfaces;
using UnityEngine;

namespace CoreGameLoop.Implementations.Systems
{
    ///  <inheritdoc />
    public class CoreGameLoopSystem : ICoreGameLoopSystem
    {
        private IScreenSystem _screenSystem;
        private IGameContainer _gameContainer;
        private IGameFieldCreator _gameFieldCreator;

        private CoreGameLoopConfig _coreGameLoopConfig;

        private bool _isInitialized;

        ///  <inheritdoc />
        public Action OnBackToMenu { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public CoreGameLoopSystem(IScreenSystem screenSystem,
            IGameContainer gameContainer,
            IGameFieldCreator gameFieldCreator,
            CoreGameLoopConfig coreGameLoopConfig)
        {
            _screenSystem = screenSystem;
            _gameContainer = gameContainer;
            _gameFieldCreator = gameFieldCreator;

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

            _screenSystem.GetScreen<IGameScreen>().TopPanel.BackButton.onClick.AddListener(PauseButtonClick);

            var pauseScreen = _screenSystem.GetScreen<IPauseScreen>();
            pauseScreen.ContinueButton.onClick.AddListener(ContinueAfterPauseButtonClick);
            pauseScreen.ToMenuButton.onClick.AddListener(ToMenuButtonClick);

            var endGameScreen = _screenSystem.GetScreen<IEndGameScreen>();
            endGameScreen.ContinueButton.onClick.AddListener(StartGameLoop);
            endGameScreen.ToMenuButton.onClick.AddListener(ToMenuButtonClick);

            _gameFieldCreator.CreateGameField();
        }

        ///  <inheritdoc />
        public void StartGameLoop()
        {
            var gameScreen = _screenSystem.ShowScreen<IGameScreen>();

            _gameFieldCreator.SetStartGameFieldCellsData();
        }

        /// <summary>
        /// Победа
        /// </summary>
        private void Win()
        {
            EndGame(true);
        }

        /// <summary>
        /// Поражение
        /// </summary>
        private void Lose()
        {
            EndGame(false);
        }

        /// <summary>
        /// Окончание игры
        /// </summary>
        /// <param name="win"></param>
        private void EndGame(bool win)
        {
            var gameScreen = _screenSystem.ShowScreen<IEndGameScreen>();
        }

        /// <summary>
        /// Нажатие кнопки паузы
        /// </summary>
        private void PauseButtonClick()
        {
            _screenSystem.ShowScreen<IPauseScreen>(false);
        }

        /// <summary>
        /// Нажатие кнопки продолжить после паузы
        /// </summary>
        private void ContinueAfterPauseButtonClick()
        {
            _screenSystem.HideScreen<IPauseScreen>();
        }

        /// <summary>
        /// Нажатие кнопки возвращения в меню
        /// </summary>
        private void ToMenuButtonClick()
        {
            OnBackToMenu?.Invoke();
        }
    }
}