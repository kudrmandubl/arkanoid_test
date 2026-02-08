using Balls.Data;
using Balls.Interfaces;
using Cameras.Interfaces;
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
        private ICameraWidthAdjuster _cameraWidthAdjuster;

        private bool _isActive;
        private float _screenLeft;
        private float _screenRight;
        private float _screenBottom;
        private float _screenTop;

        /// <summary>
        /// Конструктор
        /// </summary>
        public BallMover(IMainCamera mainCamera,
            IBallInteractor ballInteractor,
            IMonoBehaviourCycle monoBehaviourCycle,
            IBallCreator ballCreator,
            ICameraWidthAdjuster cameraWidthAdjuster)
        {
            _mainCamera = mainCamera;
            _ballInteractor = ballInteractor;
            _monoBehaviourCycle = monoBehaviourCycle;
            _ballCreator = ballCreator;
            _cameraWidthAdjuster = cameraWidthAdjuster;

            _monoBehaviourCycle.OnFixedUpdate += MoveBalls;
            _cameraWidthAdjuster.OnCameraSizeChange += RefreshScreenBorders;
        }

        ///  <inheritdoc />
        public void SetActive(bool value)
        {
            _isActive = value;
            if (value)
            {
                RefreshScreenBorders();
            }
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
                ballView.Rigidbody2D.MovePosition(position);
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
            // Учитывать ширину ракетки (scale.x) и размер
            float ballHalfWidth = ballData.Size.x / 2f;
            float ballHalfHeight = ballData.Size.y / 2f;

            isBallLost = false;

            if (position.x <= _screenLeft + ballHalfWidth
                || position.x >= _screenRight - ballHalfWidth)
            {
                ballData.Direction.x = -ballData.Direction.x;
                position.x = ClampPositionX(position.x, ballHalfWidth);
            }
            else if (position.y >= _screenTop - ballHalfHeight)
            {
                ballData.Direction.y = -ballData.Direction.y;
                position.y = ClampPositionY(position.y, ballHalfHeight); 
            }
            else if (position.y <= _screenBottom + ballHalfHeight)
            {
                isBallLost = true;
            }

            // Ограничить позицию так, чтобы ракетка не выходила за границы

            return position;
        }

        /// <summary>
        /// Обновить границы экрана
        /// </summary>
        private void RefreshScreenBorders()
        {
            //Получить границы экрана в мировых координатах
            var bottomLeftPosition = _mainCamera.Camera.ScreenToWorldPoint(new Vector3(0, 0, _mainCamera.Camera.transform.position.z));
            var topRightPosition = _mainCamera.Camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, _mainCamera.Camera.transform.position.z));
            _screenLeft = bottomLeftPosition.x;
            _screenRight = topRightPosition.x;
            _screenBottom = bottomLeftPosition.y;
            _screenTop = topRightPosition.y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionX"></param>
        /// <param name="ballHalfWidth"></param>
        /// <returns></returns>
        private float ClampPositionX(float positionX, float ballHalfWidth)
        {
            return Mathf.Clamp(positionX, _screenLeft + ballHalfWidth, _screenRight - ballHalfWidth);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="positionY"></param>
        /// <param name="ballHalfHeight"></param>
        /// <returns></returns>
        private float ClampPositionY(float positionY, float ballHalfHeight)
        {
             return Mathf.Clamp(positionY, _screenBottom + ballHalfHeight, _screenTop - ballHalfHeight);
        }
    }
}