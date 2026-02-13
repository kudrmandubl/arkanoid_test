using System;
using Balls.Configs;
using Balls.Interfaces;
using Common.Interfaces;
using GameField.Interfaces;
using Racket.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.ParticleSystem;

namespace Balls.Implementations.Systems
{
    ///  <inheritdoc />
    public class BallCollisionProcessor : IBallCollisionProcessor
    {
        private IMainCamera _mainCamera;
        private IBallInteractor _ballInteractor;
        private IBallCreator _ballCreator;
        private IGameFieldInteractor _gameFieldInteractor;
        private IGameContainer _gameContainer;
        private IMonoBehaviourPool<IGameFieldCellDestroyParticlesView> _gameFieldCellDestroyParticlesPool;

        private BallsConfig _ballsConfig;

        ///  <inheritdoc />
        public Action<IGameFieldCellView> OnDestroyGameFieldCelLView { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public BallCollisionProcessor(IMainCamera mainCamera,
            IBallInteractor ballInteractor,
            IBallCreator ballCreator,
            IGameFieldInteractor gameFieldInteractor,
            IGameContainer gameContainer,
            IMonoBehaviourPool<IGameFieldCellDestroyParticlesView> gameFieldCellDestroyParticlesPool,
            BallsConfig ballsConfig)
        {
            _mainCamera = mainCamera;
            _ballInteractor = ballInteractor;
            _ballCreator = ballCreator;
            _gameFieldInteractor = gameFieldInteractor;
            _gameContainer = gameContainer;
            _gameFieldCellDestroyParticlesPool = gameFieldCellDestroyParticlesPool;

            _ballsConfig = ballsConfig;

            _ballCreator.OnCreateBall += SubscribeToBall;
            _ballCreator.OnDestroyBall += UnsubscribeFromBall;

            _gameFieldCellDestroyParticlesPool.SetPrefab(_ballsConfig.GameFieldCellDestroyParticlesPrefab);
            _gameFieldCellDestroyParticlesPool.SetContainer(_gameContainer.CoreContainer);
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

            CreateGameFieldCellDestroyParticles(collisionPosition);
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

        /// <summary>
        /// Создать партиклы уничтожения игровой ячейки
        /// </summary>
        private void CreateGameFieldCellDestroyParticles(Vector3 position)
        {
            var particlesView = _gameFieldCellDestroyParticlesPool.GetFreeElement();
            particlesView.Transform.position = position;
            particlesView.OnParticleStop += DestroyGameFieldCellDestroyParticles;
        }

        /// <summary>
        /// Уничтожить партиклы уничтожения игровой ячейки
        /// </summary>
        /// <param name="particlesView"></param>
        private void DestroyGameFieldCellDestroyParticles(IParticlesView particlesView)
        {
            particlesView.OnParticleStop -= DestroyGameFieldCellDestroyParticles;
            _gameFieldCellDestroyParticlesPool.Free(particlesView as IGameFieldCellDestroyParticlesView);
        }
    }
}