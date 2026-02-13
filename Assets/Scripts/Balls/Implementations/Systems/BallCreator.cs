using System;
using Balls.Configs;
using Balls.Data;
using Balls.Implementations.Views;
using Balls.Interfaces;
using Common.Interfaces;
using GameField.Interfaces;
using Racket.Interfaces;
using UnityEngine;
using UnityEngine.UIElements;

namespace Balls.Implementations.Systems
{
    ///  <inheritdoc />
    public class BallCreator : IBallCreator
    {
        private IGameContainer _gameContainer;
        private IRacketInteractor _racketInteractor;
        private IBallInteractor _ballInteractor;
        private IMonoBehaviourPool<IBallView> _ballViewPool;
        private IPool<BallData> _ballDataPool;
        private IGameFieldInteractor _gameFieldInteractor;

        private BallsConfig _ballsConfig;

        ///  <inheritdoc />
        public Action<IBallView> OnCreateBall { get; set; }

        ///  <inheritdoc />
        public Action<IBallView> OnDestroyBall { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public BallCreator(IGameContainer gameContainer,
            IRacketInteractor racketInteractor,
            IBallInteractor ballInteractor,
            IMonoBehaviourPool<IBallView> ballViewPool,
            IPool<BallData> ballDataPool,
            IGameFieldInteractor gameFieldInteractor,
            BallsConfig ballsConfig)
        {
            _gameContainer = gameContainer;
            _racketInteractor = racketInteractor;
            _ballInteractor = ballInteractor;
            _ballViewPool = ballViewPool;
            _ballDataPool = ballDataPool;
            _gameFieldInteractor = gameFieldInteractor;

            _ballsConfig = ballsConfig;

            _ballViewPool.SetPrefab(_ballsConfig.BallViewPrefab);
            _ballViewPool.SetContainer(_gameContainer.CoreContainer);
        }

        ///  <inheritdoc />
        public void CreateBall()
        {
            var position = _racketInteractor.RacketView.Transform.position
                + Vector3.up * _racketInteractor.RacketView.SizableTransform.lossyScale.y * 0.5f
                + Vector3.up * _ballsConfig.BallSize.y * 0.5f
                + Vector3.up * _ballsConfig.ExtraStartBallPositionY;
            var direction = Vector3.up + Vector3.right * UnityEngine.Random.Range(-_ballsConfig.MaxBallStartDirectionX, _ballsConfig.MaxBallStartDirectionX);
            direction = direction.normalized;

            CreateBall(position, direction, _ballsConfig.BallSpeed);
        }

        ///  <inheritdoc />
        public void CreateExtraBalls(int count)
        {
            var currentBallCount = _ballInteractor.BallViews.Count;
            for (int i = 0; i < currentBallCount; i++)
            {
                var ballView = _ballInteractor.BallViews[i];
                for (int j = 0; j < count; j++)
                {
                    var direction = UnityEngine.Random.onUnitSphere;
                    direction.z = 0;
                    direction = direction.normalized; 
                    CreateBall(ballView.Transform.position, direction, ballView.BallData.Speed);
                }
            }
        }

        ///  <inheritdoc />
        public void DestroyBall(IBallView ballView)
        {
            _ballDataPool.Free(ballView.BallData);
            _ballViewPool.Free(ballView);
            _ballInteractor.RemoveBall(ballView);
            OnDestroyBall?.Invoke(ballView);
        }

        ///  <inheritdoc />
        public void DestroyAllBalls()
        {
            for (int i = _ballInteractor.BallViews.Count - 1; i >= 0; i--)
            {
                var ballView = _ballInteractor.BallViews[i];
                DestroyBall(ballView);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        /// <param name="speed"></param>
        private void CreateBall(Vector3 position, Vector3 direction, float speed)
        {
            var ballView = _ballViewPool.GetFreeElement();
            ballView.SizableTransform.localScale = Vector3.one * _ballsConfig.BallSize;
            ballView.TrailRenderer.startWidth = _ballsConfig.BallSize.x;
            ballView.Transform.position = position; 

            ballView.BallData = _ballDataPool.GetFreeElement();
            ballView.BallData.Size = _ballsConfig.BallSize;
            ballView.BallData.Direction = direction;
            ballView.BallData.Speed = speed;
            ballView.BallData.SpeedStep = _ballsConfig.MaxExtraBallSpeed / _gameFieldInteractor.TotalCellCount;

            _ballInteractor.AddBall(ballView);
            OnCreateBall?.Invoke(ballView);
        }
    }
}