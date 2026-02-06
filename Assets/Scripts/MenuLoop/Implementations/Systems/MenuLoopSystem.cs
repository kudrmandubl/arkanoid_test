using Common;
using Common.Interfaces;
using Cysharp.Threading.Tasks;
using MainMenu.Interfaces;
using MenuLoop.Interfaces;
using Screens.Interfaces;
using UnityEngine;

namespace MenuLoop.Implementations.Systems
{
    ///  <inheritdoc />
    public class MenuLoopSystem : IMenuLoopSystem
    {
        private IMainMenu _mainMenu;
        private IScreenSystem _screenSystem;
        private IMainCamera _mainCamera;

        /// <summary>
        /// Конструктор
        /// </summary>
        public MenuLoopSystem(IMainMenu mainMenu,
            IScreenSystem screenSystem,
            IMainCamera mainCamera)
        {
            _mainMenu = mainMenu;
            _screenSystem = screenSystem;
            _mainCamera = mainCamera;

            StartMenuLoopAsync();
        }

        ///  <inheritdoc />
        public async UniTask StartMenuLoopAsync()
        {
            _screenSystem.Initialize(CanvasWithKey.GameSceneCanvasKey);
            _mainCamera.Initialize(CameraWithKey.GameSceneCameraKey);

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 60;

            _mainMenu.Initialize();
            _mainMenu.ShowMainMenu();
        }
    }
}