using System;
using Balls.Interfaces;
using Buffs.Interfaces;
using Common.Interfaces;
using CoreGameLoop.Configs;
using CoreGameLoop.Implementations.Views;
using CoreGameLoop.Interfaces;
using DataStorage.Data;
using DataStorage.Interfaces;
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
        private IBuffSystem _buffSystem;

        private CoreGameLoopConfig _coreGameLoopConfig;

        private bool _isInitialized;
        private int _currentScore;
        private IGameScreen _gameScreen;
        private bool _gameEnded;
        private int _level;
        private int _triesCount;
        private bool _win;

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
            IBuffSystem buffSystem,
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
            _buffSystem = buffSystem;

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

            _buffSystem.Initialize();

            var _coreBackView = GameObject.Instantiate(_coreGameLoopConfig.CoreBackViewPrefab, _gameContainer.CoreContainer);
            _coreBackView.transform.SetAsFirstSibling();

            _gameScreen = _screenSystem.GetScreen<IGameScreen>();
            _gameScreen.TopPanel.BackButton.onClick.AddListener(PauseButtonClick);

            var pauseScreen = _screenSystem.GetScreen<IPauseScreen>();
            pauseScreen.ContinueButton.onClick.AddListener(ContinueAfterPauseButtonClick);
            pauseScreen.ToMenuButton.onClick.AddListener(ToMenuButtonClick);

            var endGameScreen = _screenSystem.GetScreen<IEndGameScreen>();
            endGameScreen.ContinueButton.onClick.AddListener(EndGameContinueButtonClick);
            endGameScreen.ToMenuButton.onClick.AddListener(ToMenuButtonClick);

            _ballCreator.OnDestroyBall += CheckLose;
            _gameFieldInteractor.OnAllGameFieldCellDestroy += Win;
            _gameFieldInteractor.OnGameFieldCellDestroy += IncreaseScore;

            _buffSystem.OnApplyAddLivesBuff += AddTriesCount;

            DropRunData();
        }

        ///  <inheritdoc />
        public void StartGameLoop()
        {
            var gameScreen = _screenSystem.ShowScreen<IGameScreen>();

            var levelConfigData = _coreGameLoopConfig.LevelConfigsData[(_level - 1) % _coreGameLoopConfig.LevelConfigsData.Length];
            _gameFieldCreator.CreateGameField(levelConfigData.GameFieldCreateParams);
            _gameFieldInteractor.Restart();
            _racketSystem.CreateRacket();
            _ballCreator.CreateBall();

            _racketSystem.SetControlActive(true);
            _ballMover.SetActive(true);
            _buffSystem.SetActive(true);
            _gameEnded = false;
        }

        /// <summary>
        /// Победа
        /// </summary>
        private void Win()
        {
            EndGame(true);
            IncreaseLevel();
        }

        /// <summary>
        /// Поражение
        /// </summary>
        private void Lose()
        {
            if (_gameEnded)
            {
                return;
            }

            AddTriesCount(-1);
            EndGame(false);
        }

        /// <summary>
        /// Окончание игры
        /// </summary>
        /// <param name="win"></param>
        private void EndGame(bool win)
        {
            var endGameScreen = _screenSystem.ShowScreen<IEndGameScreen>();
            endGameScreen.TriesCountRoot.SetActive(!win);
            endGameScreen.TriesCountText.text = $"{_triesCount}";
            endGameScreen.ResultText.text = win ? _coreGameLoopConfig.WinLabel : _coreGameLoopConfig.LoseLabel;
            endGameScreen.ContinueButtonText.text = win 
                ? _coreGameLoopConfig.ContinueLabel
                : _triesCount > 0
                ? _coreGameLoopConfig.RestartLevelLabel
                : _coreGameLoopConfig.StartNewRunLabel;

            _gameEnded = true;
            _win = win;
            _racketSystem.SetControlActive(false);
            _ballMover.SetActive(false);
            _buffSystem.SetActive(false);
            _ballCreator.DestroyAllBalls();
            _buffSystem.Clear();
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
            _buffSystem.SetActive(false);
        }

        /// <summary>
        /// Нажатие кнопки продолжить после паузы
        /// </summary>
        private void ContinueAfterPauseButtonClick()
        {
            _screenSystem.HideScreen<IPauseScreen>();
            _racketSystem.SetControlActive(true);
            _ballMover.SetActive(true);
            _buffSystem.SetActive(true);
        }

        /// <summary>
        /// Нажатие кнопки возвращения в меню
        /// </summary>
        private void ToMenuButtonClick()
        {
            _ballCreator.DestroyAllBalls();
            _buffSystem.Clear();
            OnBackToMenu?.Invoke();
            ClearScore();
            DropRunData();
        }

        /// <summary>
        /// Нажатие кнопки продолжить в окне окончания игры
        /// </summary>
        private void EndGameContinueButtonClick()
        {
            if(!_win && _triesCount <= 0)
            {
                ClearScore();
                DropRunData();
            }
            StartGameLoop();
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

        /// <summary>
        /// Увеличить уровень
        /// </summary>
        private void IncreaseLevel()
        {
            SetLevel(_level + 1);
        }

        /// <summary>
        /// Сбросить данные текущего забега
        /// </summary>
        private void DropRunData()
        {
            SetLevel(1);
            SetTriesCount(_coreGameLoopConfig.DefaultTriesCount);
        }

        /// <summary>
        /// Установить уровень
        /// </summary>
        /// <param name="level"></param>
        private void SetLevel(int level)
        {
            _level = level;
            _screenSystem.GetScreen<GameScreen>().LevelText.text = $"{level}";
        }

        /// <summary>
        /// Добавить количество попыток
        /// </summary>
        /// <param name="value"></param>
        private void AddTriesCount(int value)
        {
            SetTriesCount(_triesCount + value);
        }

        /// <summary>
        /// Установить количество попыток
        /// </summary>
        /// <param name="triesCount"></param>
        private void SetTriesCount(int triesCount)
        {
            _triesCount = triesCount;
            _screenSystem.GetScreen<GameScreen>().TriesCountText.text = $"{triesCount}";
        }
    }
}