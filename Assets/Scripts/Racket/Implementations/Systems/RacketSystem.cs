using Common.Interfaces;
using DG.Tweening;
using Inputs.Interfaces;
using Racket.Configs;
using Racket.Data;
using Racket.Interfaces;
using UnityEngine;

namespace Racket.Implementations.Systems
{
    ///  <inheritdoc />
    public class RacketSystem : IRacketSystem
    {
        private IGameContainer _gameContainer;
        private IInputCatcher _inputCatcher;
        private IMainCamera _mainCamera;

        private RacketConfig _racketConfig;

        private IRacketView _racketView;
        private bool _isActive;
        
        /// <summary>
        /// Конструктор
        /// </summary>
        public RacketSystem(IGameContainer gameContainer,
            IInputCatcher inputCatcher,
            IMainCamera mainCamera,
            RacketConfig racketConfig)
        {
            _gameContainer = gameContainer;
            _inputCatcher = inputCatcher;
            _mainCamera = mainCamera;

            _racketConfig = racketConfig;

            _inputCatcher.OnPointer += Dragging;
        }

        ///  <inheritdoc />
        public void CreateRacket()
        {
            if(_racketView == null)
            {
                _racketView = GameObject.Instantiate(_racketConfig.RacketViewPrefab, _gameContainer.CoreContainer);
                _racketView.SizableTransform.localScale = Vector2.one * _racketConfig.RacketSize;
                _racketView.RacketData = new RacketData();
            }

            _racketView.Transform.localPosition = _racketConfig.RacketPosition;
            SetRacketWidth(_racketView, _racketConfig.RacketSize.x);
        }

        ///  <inheritdoc />
        public void SetControlActive(bool value)
        {
            _isActive = value;
        }

        /// <summary>
        /// Установить ширину ракетки
        /// </summary>
        /// <param name="racketView"></param>
        /// <param name="width"></param>
        private void SetRacketWidth(IRacketView racketView, float width)
        {
            var scale = racketView.SizableTransform.localScale;
            scale.x = width;
            racketView.SizableTransform.localScale = scale;

            racketView.RacketData.Width = width;
        }

        /// <summary>
        /// Перетаскивание
        /// </summary>
        private void Dragging(Vector3 pointerPosition)
        {
            if (!_isActive)
            {
                return;
            }

            var pointerWorldPosition = _mainCamera.Camera.ScreenToWorldPoint(pointerPosition);

            var racketPosition = _racketView.Transform.position;
            racketPosition.x = pointerWorldPosition.x;
            racketPosition = ApplyBorders(racketPosition);
            _racketView.Transform.position = racketPosition;
        }

        /// <summary>
        /// Применить границы
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private Vector3 ApplyBorders(Vector3 position)
        {
            // Получить границы экрана в мировых координатах
            float screenLeft = _mainCamera.Camera.ScreenToWorldPoint(new Vector3(0, 0, _mainCamera.Camera.transform.position.z)).x;
            float screenRight = _mainCamera.Camera.ScreenToWorldPoint(new Vector3(Screen.width, 0, _mainCamera.Camera.transform.position.z)).x;

            // Учитывать ширину ракетки (scale.x) и размер
            float racketHalfWidth = _racketView.RacketData.Width / 2f;

            // Ограничить позицию так, чтобы ракетка не выходила за границы
            position.x = Mathf.Clamp(position.x, screenLeft + racketHalfWidth, screenRight - racketHalfWidth);

            return position;
        }
    }
}