using System;
using Balls.Interfaces;
using GameField.Interfaces;
using Racket.Interfaces;
using UnityEngine;

namespace Balls.Implementations.Systems
{
    ///  <inheritdoc />
    public class BallCollisionProcessor : IBallCollisionProcessor
    {
        private IBallCreator _ballCreator;
        private IGameFieldInteractor _gameFieldInteractor;

        /// <summary>
        /// Конструктор
        /// </summary>
        public BallCollisionProcessor(IBallCreator ballCreator,
            IGameFieldInteractor gameFieldInteractor)
        {
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
            var collisionPosition = gameFieldCellView.Collider2D.bounds.ClosestPoint(ballView.Transform.position);
            var direction = ballView.Transform.position - collisionPosition;

            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                if (!ballView.BallData.IsCanCollideX )
                {
                    return;
                }

                ballView.BallData.Direction.x = -ballView.BallData.Direction.x;
                ballView.BallData.IsCanCollideX = false;
            }
            else
            {
                if (!ballView.BallData.IsCanCollideY)
                {
                    return;
                }

                ballView.BallData.Direction.y = -ballView.BallData.Direction.y;
                ballView.BallData.IsCanCollideY = false;
            }

            _gameFieldInteractor.DestroyCellByBall(gameFieldCellView, collisionPosition);

            ballView.BallData.Speed += ballView.BallData.SpeedStep;
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
            var isBallInRacket = ballView.Transform.position.Equals(collisionPosition);


            if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y) 
                && !isBallInRacket)
            {
                ballView.BallData.Direction.x = -ballView.BallData.Direction.x;
            }
            else
            {
                var racketDirection = ballView.Transform.position - racketView.Transform.position;
                ballView.BallData.Direction.x = racketDirection.x * 2 / racketView.RacketData.Width;
                ballView.BallData.Direction.y = 1;
                ballView.BallData.Direction = ballView.BallData.Direction.normalized;

                if (isBallInRacket)
                {
                    var ballPosition = ballView.Transform.position;
                    ballPosition.y += ballView.BallData.Size.y + racketView.RacketData.Height;
                    ballView.Rigidbody2D.MovePosition(ballPosition);
                }
            }
        }
    }
}