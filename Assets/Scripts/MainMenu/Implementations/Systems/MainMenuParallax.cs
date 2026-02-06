using Common.Interfaces;
using MainMenu.Configs;
using MainMenu.Interfaces;
using Screens.Interfaces;
using UnityEngine;

namespace MainMenu.Implementations.Systems
{
    ///  <inheritdoc />
    public class MainMenuParallax : IMainMenuParallax
    {
        private IMonoBehaviourCycle _monoBehaviourCycle;
        private IScreenSystem _screenSystem;
        private IMainCamera _mainCamera;

        private MainMenuConfig _mainMenuConfig;

        private IMainMenuScreen _mainMenuScreen;
        private bool _isActive;
        private Vector3 _frontStartPosition;
        private Vector3 _backStartPosition;
        private bool _isStartPositionSetted;

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainMenuParallax(IMonoBehaviourCycle monoBehaviourCycle,
            IScreenSystem screenSystem,
            IMainCamera mainCamera,
            MainMenuConfig mainMenuConfig)
        {
            _monoBehaviourCycle = monoBehaviourCycle;
            _screenSystem = screenSystem;
            _mainCamera = mainCamera;

            _mainMenuConfig = mainMenuConfig;

            _monoBehaviourCycle.OnUpdate += ProcessParallax;
        }

        ///  <inheritdoc />
        public void Initialize()
        {
            _mainMenuScreen = _screenSystem.GetScreen<IMainMenuScreen>();
        }

        ///  <inheritdoc />
        public void SetActive(bool value)
        {
            _isActive = value;
        }

        /// <summary>
        /// Обработать параллакс
        /// </summary>
        /// <param name="deltaTime"></param>
        private void ProcessParallax(float deltaTime)
        {
            if(!_isActive || _mainMenuScreen == null)
            {
                return;
            }

            if (!_isStartPositionSetted)
            {
                _frontStartPosition = _mainMenuScreen.FrontPart.localPosition;
                _backStartPosition = _mainMenuScreen.BackPart.localPosition;
                _isStartPositionSetted = true;
            }

            // Получаем позицию курсора в экранных координатах
            var mousePosition = Input.mousePosition;
            var viewportPosition = _mainCamera.Camera.ScreenToViewportPoint(mousePosition);
            viewportPosition = Vector3.ClampMagnitude(viewportPosition, 1);
            // позиция от -1 до 1
            viewportPosition = viewportPosition * 2 - Vector3.one;
            viewportPosition.z = 0;

            deltaTime *= _mainMenuConfig.ParallaxSpeed;

            ApplyParallax(_mainMenuScreen.FrontPart.transform, 
                _frontStartPosition,
                viewportPosition,
                _mainMenuConfig.FrontMaxParallaxOffset,
                deltaTime);
            ApplyParallax(_mainMenuScreen.BackPart,
                _backStartPosition,
                viewportPosition,
                _mainMenuConfig.BackMaxParallaxOffset,
                deltaTime);

        }

        /// <summary>
        /// Применить параллакс
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="startPosition"></param>
        /// <param name="viewportPosition"></param>
        /// <param name="maxOffset"></param>
        /// <param name="deltaTime"></param>
        private void ApplyParallax(Transform transform, 
            Vector3 startPosition, 
            Vector3 viewportPosition,
            float maxOffset,
            float deltaTime)
        {
            // Получаем текущий локальный позицию объекта
            var currentPosition = transform.localPosition;
            // Можно плавно смещать, например, с помощью lerp
            var newPosition = Vector3.Lerp(currentPosition, startPosition + viewportPosition * maxOffset, deltaTime);
            // Применяем новое смещение
            transform.localPosition = newPosition;

        }
    }
}