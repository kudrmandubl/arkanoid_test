using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Screens.Configs;
using Screens.Interfaces;
using UnityEngine;
using IScreen = Screens.Interfaces.IScreen;

namespace Screens.Implementations.Systems
{
    ///  <inheritdoc />
    public class ScreenSystem : IScreenSystem
    {
        private ScreensConfig _screensConfig;

        private Transform _canvasTransform;
        private List<IScreen> _screenInstances;
        private Stack<IScreen> _currentScreens;

        ///  <inheritdoc />
        public Transform CanvasTransform => _canvasTransform;

        ///  <inheritdoc />
        public Action<IScreen> OnCreateScreen {  get; set; }

        ///  <inheritdoc />
        public List<IScreen> ScreenInstances => _screenInstances;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="screensConfig"></param>
        public ScreenSystem(ScreensConfig screensConfig) 
        {
            _screensConfig = screensConfig;

            _screenInstances = new List<IScreen>();
            _currentScreens = new Stack<IScreen>();
        }

        ///  <inheritdoc />
        public void Initialize(string canvasKey)
        {
            _canvasTransform = GameObject.FindObjectsByType<CanvasWithKey>(FindObjectsSortMode.None)
                .FirstOrDefault(x => x.Key == canvasKey)
                .transform;
        }

        ///  <inheritdoc />
        public T GetScreen<T>() where T : IScreen
        {
            var instance = _screenInstances.FirstOrDefault(x => x is T);
            if(instance != null)
            {
                return (T) instance;
            }

            var prefab = _screensConfig.ScreenPrefabs.FirstOrDefault(x => x is T);
            if (prefab == null)
            {
                Debug.LogError($"Не найден экран {typeof(T)} в списке префабов экранов");
                return default(T);
            }

            var newInstance = GameObject.Instantiate(prefab, _canvasTransform);
            instance = newInstance.GetComponent<T>();

            _screenInstances.Add(instance);
            SetActiveScreen(instance, false);

            OnCreateScreen?.Invoke(instance);

            return (T) instance;
        }

        ///  <inheritdoc />
        public IScreen GetCurrentScreen()
        {
            return _currentScreens.First();
        }

        ///  <inheritdoc />
        public IScreen HideScreen<T>() where T : IScreen
        {
            var screen = GetScreen<T>();
            if (screen == null)
            {
                Debug.LogError($"Не возможно скрыть экран {nameof(T)}, т.к. он не найден");
                return null;
            }

            if(GetCurrentScreen() is not T)
            {
                Debug.LogError($"Не возможно скрыть экран {nameof(T)}, т.к. он не последний в очереди показа");
                return null;
            }

            _currentScreens.Pop();
            SetActiveScreen(screen, false);
            return screen;
        }

        ///  <inheritdoc />
        public T ShowScreen<T>(bool showAsSingle = true) where T : IScreen
        {
            var screen = GetScreen<T>();
            if (screen == null)
            {
                Debug.LogError($"Не возможно показать экран {nameof(T)}");
                return default;
            }

            if (showAsSingle)
            {
                while (_currentScreens.Count > 0)
                {
                    var shownScreen = _currentScreens.Pop();
                    SetActiveScreen(shownScreen, false);
                }
            }
            else
            {
                screen.Transform.SetAsLastSibling();
            }
            
            _currentScreens.Push(screen);

            SetActiveScreen(screen, true);
            return screen;
        }

        /// <summary>
        /// Установить активность экрана
        /// </summary>
        /// <param name="screen">Экран</param>
        /// <param name="value">Флаг активности</param>
        private void SetActiveScreen(IScreen screen, bool value)
        {
            screen.SetActive(value);
        }
    }
}
