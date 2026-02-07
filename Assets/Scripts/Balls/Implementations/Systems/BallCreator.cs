using System;
using Balls.Configs;
using Balls.Data;
using Balls.Interfaces;
using Common.Interfaces;
using Racket.Interfaces;
using UnityEngine;

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

        private BallsConfig _ballsConfig;

        ///  <inheritdoc />
        public Action OnDestroyBall { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public BallCreator(IGameContainer gameContainer,
            IRacketInteractor racketInteractor,
            IBallInteractor ballInteractor,
            IMonoBehaviourPool<IBallView> ballViewPool,
            IPool<BallData> ballDataPool,
            BallsConfig ballsConfig)
        {
            _gameContainer = gameContainer;
            _racketInteractor = racketInteractor;
            _ballInteractor = ballInteractor;
            _ballViewPool = ballViewPool;
            _ballDataPool = ballDataPool;

            _ballsConfig = ballsConfig;

            _ballViewPool.SetPrefab(_ballsConfig.BallViewPrefab);
            _ballViewPool.SetContainer(_gameContainer.CoreContainer);
        }

        ///  <inheritdoc />
        public void CreateBall()
        {
            var ballView = _ballViewPool.GetFreeElement();
            ballView.SizableTransform.localScale = Vector3.one * _ballsConfig.BallSize;
            ballView.Transform.position = _racketInteractor.RacketView.Transform.position 
                + Vector3.up * _racketInteractor.RacketView.SizableTransform.lossyScale.y * 0.5f
                + Vector3.up * _ballsConfig.BallSize.y * 0.5f;
            
            ballView.BallData = _ballDataPool.GetFreeElement();
            ballView.BallData.Size = _ballsConfig.BallSize;
            ballView.BallData.Direction = Vector3.up + Vector3.right * UnityEngine.Random.Range(-_ballsConfig.MaxBallStartDirectionX, _ballsConfig.MaxBallStartDirectionX);
            ballView.BallData.Speed = _ballsConfig.BallSpeed;

            _ballInteractor.AddBall(ballView);
        }

        ///  <inheritdoc />
        public void DestroyBall(IBallView ballView)
        {
            _ballDataPool.Free(ballView.BallData);
            _ballViewPool.Free(ballView);
            _ballInteractor.RemoveBall(ballView);
            OnDestroyBall?.Invoke();
        }
    }
}