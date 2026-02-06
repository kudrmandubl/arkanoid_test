using Common.Configs;
using Common.Interfaces;
using CoreGameLoop.Interfaces;
using MainMenu.Configs;
using MainMenu.Interfaces;
using Screens.Interfaces;

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

        private MainMenuConfig _mainMenuConfig;
        private TweenAnimationsConfig _tweenAnimationsConfig;

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainMenu(IScreenSystem screenSystem,
            ICoreGameLoopSystem coreGameLoopSystem,
            IGameContainer gameContainer,
            IMainCamera mainCamera,
            IMainMenuParallax mainMenuParallax,
            MainMenuConfig mainMenuConfig,
            TweenAnimationsConfig tweenAnimationsConfig)
        {
            _screenSystem = screenSystem;
            _coreGameLoopSystem = coreGameLoopSystem;
            _gameContainer = gameContainer;
            _mainCamera = mainCamera;
            _mainMenuParallax = mainMenuParallax;

            _mainMenuConfig = mainMenuConfig;
            _tweenAnimationsConfig = tweenAnimationsConfig;
        }

        ///  <inheritdoc />
        public void Initialize()
        {
            var mainMenuScreen = _screenSystem.GetScreen<IMainMenuScreen>();
            mainMenuScreen.PlayButton.onClick.AddListener(PlayButtonClick);
            _screenSystem.GetScreen<IGameScreen>().TopPanel.BackButton.onClick.AddListener(BackFromCoreButtonClick);

            _mainMenuParallax.Initialize();
        }

        ///  <inheritdoc />
        public void ShowMainMenu()
        {
            _gameContainer.CoreContainer.gameObject.SetActive(false);

            FillMainMenuScreen();
            _screenSystem.ShowScreen<IMainMenuScreen>();
            _mainMenuParallax.SetActive(true);
        }

        /// <summary>
        /// Нажатие кнопки Играть
        /// </summary>
        private void PlayButtonClick()
        {
            _gameContainer.CoreContainer.gameObject.SetActive(true);

            _coreGameLoopSystem.StartGameLoop();
            _mainMenuParallax.SetActive(false);
        }

        /// <summary>
        /// Нажатие кнопки выхода из кора
        /// </summary>
        private void BackFromCoreButtonClick()
        {
            _coreGameLoopSystem.StopGameLoop();
            ShowMainMenu();
        }

        /// <summary>
        /// Заполнить экран основного меню
        /// </summary>
        private void FillMainMenuScreen()
        {

        }
    }
}