using System;
using Balls.Interfaces;
using Common.Interfaces;
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

        ///  <inheritdoc />
        public Action<IGameFieldCellView> OnDestroyGameFieldCelLView { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public BallCollisionProcessor(IMainCamera mainCamera,
            IBallInteractor ballInteractor,
            IBallCreator ballCreator,
            IGameFieldInteractor gameFieldInteractor)
        {
            _mainCamera = mainCamera;
            _ballInteractor = ballInteractor;
            _ballCreator = ballCreator;
            _gameFieldInteractor = gameFieldInteractor;

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
        private void ProcessBallCollideGameFieldCell(IBallView ballView, IGameFieldCellView gameFieldCellView)
        {
            if (ballView.BallData.IsCanCollide)
            {
                return;
            }
            ballView.BallData.IsCanCollide = true;

            var collisionPosition = gameFieldCellView.Collider2D.bounds.ClosestPoint(ballView.Transform.position);
            var direction = ballView.Transform.position - collisionPosition;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                ballView.BallData.Direction.x = -ballView.BallData.Direction.x;
            }
            else
            {
                ballView.BallData.Direction.y = -ballView.BallData.Direction.y;
            }

            _gameFieldInteractor.SetCellActive(gameFieldCellView, false);

            ballView.BallData.Speed += ballView.BallData.SpeedStep;
            OnDestroyGameFieldCelLView?.Invoke(gameFieldCellView);
        }

        /// <summary>
        /// Обработать столкновение шарика с ячейкой игрового поля
        /// </summary>
        /// <param name="ballView"></param>
        /// <param name="racketView"></param>
        private void ProcessBallCollideRacket(IBallView ballView, IRacketView racketView)
        {
            var collisionPosition = racketView.Collider2D.bounds.ClosestPoint(ballView.Transform.position);
            var direction = ballView.Transform.position - collisionPosition;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                ballView.BallData.Direction.x = -ballView.BallData.Direction.x;
            }
            else
            {
                var racketDirection = ballView.Transform.position - racketView.Transform.position;
                ballView.BallData.Direction.x = racketDirection.x * 2 / racketView.RacketData.Width;
                ballView.BallData.Direction.y = -ballView.BallData.Direction.y;
                ballView.BallData.Direction = ballView.BallData.Direction.normalized;
            }
        }
    }
}