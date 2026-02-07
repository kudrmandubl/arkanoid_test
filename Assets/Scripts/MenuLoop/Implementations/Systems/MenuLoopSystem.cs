using Common;
using Common.Interfaces;
using CoreGameLoop.Interfaces;
using Cysharp.Threading.Tasks;
using DataStorage.Interfaces;
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
        private IDataStorageSystem _dataStorageSystem;

        /// <summary>
        /// Конструктор
        /// </summary>
        public MenuLoopSystem(IMainMenu mainMenu,
            IScreenSystem screenSystem,
            IMainCamera mainCamera,
            IDataStorageSystem dataStorageSystem)
        {
            _mainMenu = mainMenu;
            _screenSystem = screenSystem;
            _mainCamera = mainCamera;
            _dataStorageSystem = dataStorageSystem;

            StartMenuLoopAsync();
        }

        ///  <inheritdoc />
        public async UniTask StartMenuLoopAsync()
        {
            _screenSystem.Initialize(CanvasWithKey.GameSceneCanvasKey);
            _mainCamera.Initialize(CameraWithKey.GameSceneCameraKey);

            _screenSystem.ShowScreen<ISimpleLoadingScreen>();

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 60;

            await _dataStorageSystem.LoadAsync();

            _mainMenu.Initialize();
            _mainMenu.ShowMainMenu();
        }
    }
}