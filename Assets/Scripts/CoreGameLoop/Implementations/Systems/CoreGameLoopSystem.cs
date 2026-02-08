using System;
using Balls.Interfaces;
using Common.Interfaces;
using CoreGameLoop.Configs;
using CoreGameLoop.Implementations.Views;
using CoreGameLoop.Interfaces;
using DataStorage.Data;
using DataStorage.Interfaces;
using GameField.Implementations.Systems;
using GameField.Interfaces;
using Racket.Interfaces;
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
        private IRacketSystem _racketSystem;
        private IBallCreator _ballCreator;
        private IBallMover _ballMover;
        private IBallInteractor _ballInteractor;
        private IBallCollisionProcessor _ballCollisionProcessor;
        private IGameFieldInteractor _gameFieldInteractor;
        private IDataStorageSystem _dataStorageSystem;

        private CoreGameLoopConfig _coreGameLoopConfig;

        private bool _isInitialized;
        private int _currentScore;
        private IGameScreen _gameScreen;

        ///  <inheritdoc />
        public Action OnBackToMenu { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public CoreGameLoopSystem(IScreenSystem screenSystem,
            IGameContainer gameContainer,
            IGameFieldCreator gameFieldCreator,
            IRacketSystem racketSystem,
            IBallCreator ballCreator,
            IBallMover ballMover,
            IBallInteractor ballInteractor,
            IBallCollisionProcessor ballCollisionProcessor,
            IGameFieldInteractor gameFieldInteractor,
            IDataStorageSystem dataStorageSystem,
            CoreGameLoopConfig coreGameLoopConfig)
        {
            _screenSystem = screenSystem;
            _gameContainer = gameContainer;
            _gameFieldCreator = gameFieldCreator;
            _racketSystem = racketSystem;
            _ballCreator = ballCreator;
            _ballMover = ballMover;
            _ballInteractor = ballInteractor;
            _ballCollisionProcessor = ballCollisionProcessor;
            _gameFieldInteractor = gameFieldInteractor;
            _dataStorageSystem = dataStorageSystem;

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

            _gameScreen = _screenSystem.GetScreen<IGameScreen>();
            _gameScreen.TopPanel.BackButton.onClick.AddListener(PauseButtonClick);

            var pauseScreen = _screenSystem.GetScreen<IPauseScreen>();
            pauseScreen.ContinueButton.onClick.AddListener(ContinueAfterPauseButtonClick);
            pauseScreen.ToMenuButton.onClick.AddListener(ToMenuButtonClick);

            var endGameScreen = _screenSystem.GetScreen<IEndGameScreen>();
            endGameScreen.ContinueButton.onClick.AddListener(StartGameLoop);
            endGameScreen.ToMenuButton.onClick.AddListener(ToMenuButtonClick);

            _ballCreator.OnDestroyBall += CheckLose;
            _gameFieldInteractor.OnAllGameFieldCellDestroy += Win;
            _gameFieldInteractor.OnGameFieldCellDestroy += IncreaseScore;
        }

        ///  <inheritdoc />
        public void StartGameLoop()
        {
            var gameScreen = _screenSystem.ShowScreen<IGameScreen>();

            _gameFieldCreator.CreateGameField();
            _gameFieldInteractor.Restart();
            _racketSystem.CreateRacket();
            _ballCreator.CreateBall();

            _racketSystem.SetControlActive(true);
            _ballMover.SetActive(true);
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
            ClearScore();
        }

        /// <summary>
        /// Окончание игры
        /// </summary>
        /// <param name="win"></param>
        private void EndGame(bool win)
        {
            var endGameScreen = _screenSystem.ShowScreen<IEndGameScreen>();
            endGameScreen.ResultText.text = win ? _coreGameLoopConfig.WinLabel : _coreGameLoopConfig.LoseLabel;
            
            _racketSystem.SetControlActive(false);
            _ballMover.SetActive(false);
            RefreshHighScore();
        }

        /// <summary>
        /// Нажатие кнопки паузы
        /// </summary>
        private void PauseButtonClick()
        {
            _screenSystem.ShowScreen<IPauseScreen>(false);
            _racketSystem.SetControlActive(false);
            _ballMover.SetActive(false);
        }

        /// <summary>
        /// Нажатие кнопки продолжить после паузы
        /// </summary>
        private void ContinueAfterPauseButtonClick()
        {
            _screenSystem.HideScreen<IPauseScreen>();
            _racketSystem.SetControlActive(true);
            _ballMover.SetActive(true);
        }

        /// <summary>
        /// Нажатие кнопки возвращения в меню
        /// </summary>
        private void ToMenuButtonClick()
        {
            _ballCreator.DestroyAllBalls();
            OnBackToMenu?.Invoke();
            ClearScore();
        }

        /// <summary>
        /// Проверить проигрыш
        /// </summary>
        private void CheckLose(IBallView ballView)
        {
            if(_ballInteractor.BallViews.Count > 0)
            {
                return;
            }

            Lose();
        }

        /// <summary>
        /// Увеличить счёт
        /// </summary>
        private void IncreaseScore()
        {
            _currentScore++;
            _gameScreen.ScoreText.text = $"{_currentScore}";
        }

        /// <summary>
        /// Очистить счёт
        /// </summary>
        private void ClearScore()
        {
            _currentScore = 0;
            _gameScreen.ScoreText.text = $"{_currentScore}";
        }

        /// <summary>
        /// Обновить максимальный счёт
        /// </summary>
        private void RefreshHighScore()
        {
            var playerStorageData = _dataStorageSystem.GetStorageData<PlayerStorageData>();
            if(_currentScore > playerStorageData.HighScore)
            {
                playerStorageData.HighScore = _currentScore;
                _dataStorageSystem.Save<PlayerStorageData>();
            }
        }
    }
}