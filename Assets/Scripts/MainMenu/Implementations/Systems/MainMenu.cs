using Common.Configs;
using Common.Interfaces;
using CoreGameLoop.Interfaces;
using DataStorage.Data;
using DataStorage.Interfaces;
using DG.Tweening;
using MainMenu.Configs;
using MainMenu.Interfaces;
using Screens.Interfaces;
using UnityEngine;

namespace MainMenu.Implementations.Systems
{
    ///  <inheritdoc />
    public class MainMenu : IMainMenu
    {
        private IScreenSystem _screenSystem;
        private ICoreGameLoopSystem _coreGameLoopSystem;
        private IGameContainer _gameContainer;
        private IMainCamera _mainCamera;
        private IMainMenuParallax _mainMenuParallax;
        private IDataStorageSystem _dataStorageSystem;

        private MainMenuConfig _mainMenuConfig;
        private TweenAnimationsConfig _tweenAnimationsConfig;

        private Tween _showMainMenuTween;
        private Tween _startCoreGameTween;

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainMenu(IScreenSystem screenSystem,
            ICoreGameLoopSystem coreGameLoopSystem,
            IGameContainer gameContainer,
            IMainCamera mainCamera,
            IMainMenuParallax mainMenuParallax,
            IDataStorageSystem dataStorageSystem,
            MainMenuConfig mainMenuConfig,
            TweenAnimationsConfig tweenAnimationsConfig)
        {
            _screenSystem = screenSystem;
            _coreGameLoopSystem = coreGameLoopSystem;
            _gameContainer = gameContainer;
            _mainCamera = mainCamera;
            _mainMenuParallax = mainMenuParallax;
            _dataStorageSystem = dataStorageSystem;

            _mainMenuConfig = mainMenuConfig;
            _tweenAnimationsConfig = tweenAnimationsConfig;
        }

        ///  <inheritdoc />
        public void Initialize()
        {
            var mainMenuScreen = _screenSystem.GetScreen<IMainMenuScreen>();
            mainMenuScreen.PlayButton.onClick.AddListener(PlayButtonClick);

            _mainMenuParallax.Initialize();

            _coreGameLoopSystem.OnBackToMenu += BackFromCore;
        }

        ///  <inheritdoc />
        public void ShowMainMenu()
        {
            if (_showMainMenuTween != null)
            {
                return;
            }

            _gameContainer.CoreContainer.gameObject.SetActive(false);

            FillMainMenuScreen();
            _screenSystem.ShowScreen<IMainMenuScreen>();
            _mainMenuParallax.SetActive(true);

            var showMainMenuTweenAnimationConfig = _tweenAnimationsConfig.GetTweenAnimationConfig<ShowMainMenuTweenAnimationConfig>();
            var mainMenuScreen = _screenSystem.GetScreen<IMainMenuScreen>();
            var anchoredPosition = mainMenuScreen.BackImageRectTransform.anchoredPosition;
            anchoredPosition.y = -Screen.height;
            mainMenuScreen.BackImageRectTransform.anchoredPosition = anchoredPosition;

            _showMainMenuTween = mainMenuScreen.BackImageRectTransform.DOAnchorPosY(0, showMainMenuTweenAnimationConfig.Duration)
                .OnComplete(() =>
                {
                    _showMainMenuTween = null;
                });
        }

        /// <summary>
        /// Нажатие кнопки Играть
        /// </summary>
        private void PlayButtonClick()
        {
            if(_startCoreGameTween != null)
            {
                return;
            }

            var startCoreGameTweenAnimationConfig = _tweenAnimationsConfig.GetTweenAnimationConfig<StartCoreGameTweenAnimationConfig>();
            var mainMenuScreen = _screenSystem.GetScreen<IMainMenuScreen>();

            _startCoreGameTween = mainMenuScreen.BackImageRectTransform.DOAnchorPosY(-Screen.height, startCoreGameTweenAnimationConfig.Duration)
                .OnComplete(() =>
                {
                    _coreGameLoopSystem.Initialize();

                    _gameContainer.CoreContainer.gameObject.SetActive(true);

                    _coreGameLoopSystem.StartGameLoop();
                    _mainMenuParallax.SetActive(false);

                    _startCoreGameTween = null;
                });
        }

        /// <summary>
        /// Возвращение из кора
        /// </summary>
        private void BackFromCore()
        {
            ShowMainMenu();
        }

        /// <summary>
        /// Заполнить экран основного меню
        /// </summary>
        private void FillMainMenuScreen()
        {
            var playerStorageData = _dataStorageSystem.GetStorageData<PlayerStorageData>();
            _screenSystem.GetScreen<IMainMenuScreen>().HighScoreText.text = $"{playerStorageData.HighScore}";
        }
    }
}