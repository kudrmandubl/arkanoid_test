using Balls.Configs;
using Balls.Data;
using Balls.Interfaces;
using Common.Interfaces;
using GameField.Configs;
using GameField.Implementations.Views;
using GameField.Interfaces;
using Racket.Interfaces;
using UnityEngine;

namespace Balls.Implementations.Systems
{
    ///  <inheritdoc />
    public class BallCollisionProcessor : IBallCollisionProcessor
    {
        private IMainCamera _mainCamera;
        private IBallInteractor _ballInteractor;
        private IBallCreator _ballCreator;
        private IGameFieldInteractor _gameFieldInteractor;

        private GameFieldConfig _gameFieldConfig;

        private bool _isActive;

        /// <summary>
        /// Конструктор
        /// </summary>
        public BallCollisionProcessor(IMainCamera mainCamera,
            IBallInteractor ballInteractor,
            IBallCreator ballCreator,
            IGameFieldInteractor gameFieldInteractor,
            GameFieldConfig gameFieldConfig)
        {
            _mainCamera = mainCamera;
            _ballInteractor = ballInteractor;
            _ballCreator = ballCreator;
            _gameFieldInteractor = gameFieldInteractor;

            _gameFieldConfig = gameFieldConfig;

            _ballCreator.OnCreateBall += SubscribeToBall;
            _ballCreator.OnDestroyBall += UnsubscribeFromBall;
        }

        /// <summary>
        /// Подписаться на шарик
        /// </summary>
        /// <param name="ballView"></param>
        private void SubscribeToBall(IBallView ballView)
        {
            ballView.OnGameFieldCellTriggerEnter += ProcessBallCollideGameFieldCell;
            ballView.OnRacketTriggerEnter += ProcessBallCollideRacket;
        }

        /// <summary>
        /// Отписаться от шарика
        /// </summary>
        /// <param name="ballView"></param>
        private void UnsubscribeFromBall(IBallView ballView)
        {
            ballView.OnGameFieldCellTriggerEnter -= ProcessBallCollideGameFieldCell;
            ballView.OnRacketTriggerEnter -= ProcessBallCollideRacket;
        }

        /// <summary>
        /// Обработать столкновение шарика с ячейкой игрового поля
        /// </summary>
        /// <param name="ballView"></param>
        /// <param name="gameFieldCellView"></param>
        private void ProcessBallCollideGameFieldCell(IBallView ballView, IGameFieldCellView gameFieldCellView, Collider2D collision)
        {
            if (ballView.BallData.IsCanCollide)
            {
                return;
            }
            ballView.BallData.IsCanCollide = true;

            _gameFieldInteractor.SetCellActive(gameFieldCellView, false);

            //Vector2 entryPoint = collision.transform.position;
            //Vector2 centerPoint = gameFieldCellView.Transform.position;
            //Debug.DrawRay(entryPoint, Vector3.up, Color.yellow, 1);
            //Debug.DrawRay(centerPoint, Vector3.up, Color.red, 1);

            //// Находим ближайшую точку на коллайдере к центру входящего объекта
            //Vector2 closestPoint = collision.ClosestPoint(centerPoint);
            //Debug.DrawRay(closestPoint, Vector3.up, Color.blue, 1);

            //// Вектор от входящего объекта к ближайшей точке
            //Vector2 direction = closestPoint - centerPoint;

            //if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            //{
            //    ballView.BallData.Direction.x = -ballView.BallData.Direction.x;
            //}
            //else
            //{
            //    ballView.BallData.Direction.y = -ballView.BallData.Direction.y;
            //}

            // Определяем сторону столкновения
            Vector2 collisionDirection = (ballView.Transform.position - gameFieldCellView.Transform.position).normalized;

            if (Mathf.Abs(collisionDirection.y) < _gameFieldConfig.GameFieldCellSize.y * 0.5f + ballView.BallData.Size.y * 0.5f)
            {
                Debug.Log($"collisionDirection = {collisionDirection}  _gameFieldConfig.GameFieldCellSize.y * 0.5f + ballView.BallData.Size.y * 0.5f = {_gameFieldConfig.GameFieldCellSize.y * 0.5f + ballView.BallData.Size.y * 0.5f}");
                ballView.BallData.Direction.x = -ballView.BallData.Direction.x;
            }
            else
            {
                ballView.BallData.Direction.y = -ballView.BallData.Direction.y;
            }
        }

        /// <summary>
        /// Обработать столкновение шарика с ячейкой игрового поля
        /// </summary>
        /// <param name="ballView"></param>
        /// <param name="racketView"></param>
        private void ProcessBallCollideRacket(IBallView ballView, IRacketView racketView, Collider2D collision)
        {
            Vector2 entryPoint = collision.transform.position;
            Vector2 centerPoint = ballView.Transform.position;
            Vector2 racketCenterPoint = racketView.Transform.position;

            // Находим ближайшую точку на коллайдере к центру входящего объекта
            Vector2 closestPoint = collision.ClosestPoint(centerPoint);

            // Вектор от входящего объекта к ближайшей точке
            Vector2 direction = closestPoint - centerPoint;
            Vector2 racketDirection = centerPoint - racketCenterPoint;


            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                ballView.BallData.Direction.x = -ballView.BallData.Direction.x;
            }
            else
            {
                ballView.BallData.Direction.x = racketDirection.x * 2 / racketView.RacketData.Width;
                ballView.BallData.Direction.y = -ballView.BallData.Direction.y;
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