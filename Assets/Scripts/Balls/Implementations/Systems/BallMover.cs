using Balls.Data;
using Balls.Interfaces;
using Common.Interfaces;
using UnityEngine;

namespace Balls.Implementations.Systems
{
    ///  <inheritdoc />
    public class BallMover : IBallMover
    {
        private IMainCamera _mainCamera;
        private IBallInteractor _ballInteractor;
        private IMonoBehaviourCycle _monoBehaviourCycle;
        private IBallCreator _ballCreator;

        private bool _isActive;

        /// <summary>
        /// Конструктор
        /// </summary>
        public BallMover(IMainCamera mainCamera,
            IBallInteractor ballInteractor,
            IMonoBehaviourCycle monoBehaviourCycle,
            IBallCreator ballCreator)
        {
            _mainCamera = mainCamera;
            _ballInteractor = ballInteractor;
            _monoBehaviourCycle = monoBehaviourCycle;
            _ballCreator = ballCreator;

            _monoBehaviourCycle.OnFixedUpdate += MoveBalls;
        }

        ///  <inheritdoc />
        public void SetActive(bool value)
        {
            _isActive = value;
        }

        /// <summary>
        /// Переместить шарики
        /// </summary>
        private void MoveBalls(float deltaTime)
        {
            if (!_isActive)
            {
                return;
            }

            for (int i = _ballInteractor.BallViews.Count - 1; i >= 0; i--)
            {
                var ballView = _ballInteractor.BallViews[i];
                var position = ballView.Transform.position + ballView.BallData.Direction * ballView.BallData.Speed * deltaTime;
                position = CheckAndApplyBorders(position, ballView.BallData, out var isBallLost);
                ballView.Transform.position = position;
                ballView.BallData.IsCanCollide = false;

                if (isBallLost)
                {
                    _ballCreator.DestroyBall(ballView);
                }
            }
        }

        /// <summary>
        /// Применить границы
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private Vector3 CheckAndApplyBorders(Vector3 position, BallData ballData, out bool isBallLost)
        {
            //Получить границы экрана в мировых координатах
            var bottomLeftPosition = _mainCamera.Camera.ScreenToWorldPoint(new Vector3(0, 0, _mainCamera.Camera.transform.position.z));
            var topRightPosition = _mainCamera.Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _mainCamera.Camera.transform.position.z));
            float screenLeft = bottomLeftPosition.x;
            float screenRight = topRightPosition.x;
            float screenBottom = bottomLeftPosition.y;
            float screenTop = topRightPosition.y;

            // Учитывать ширину ракетки (scale.x) и размер
            float ballHalfWidth = ballData.Size.x / 2f;
            float ballHalfHeight = ballData.Size.y / 2f;

            isBallLost = false;

            if (position.x <= screenLeft + ballHalfWidth
                || position.x >= screenRight - ballHalfWidth)
            {
                ballData.Direction.x = -ballData.Direction.x;
            }
            else if (position.y >= screenTop - ballHalfHeight)
            {
                ballData.Direction.y = -ballData.Direction.y;
            }
            else if (position.y <= screenBottom + ballHalfHeight)
            {
                isBallLost = true;
            }

            // Ограничить позицию так, чтобы ракетка не выходила за границы
            position.x = Mathf.Clamp(position.x, screenLeft + ballHalfWidth, screenRight - ballHalfWidth);
            position.y = Mathf.Clamp(position.y, screenBottom + ballHalfHeight, screenTop - ballHalfHeight);

            return position;
        }
    }
}