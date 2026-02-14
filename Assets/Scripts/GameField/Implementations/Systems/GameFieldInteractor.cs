using System;
using Common.Interfaces;
using GameField.Configs;
using GameField.Interfaces;
using UnityEngine;

namespace GameField.Implementations.Systems
{
    ///  <inheritdoc />
    public class GameFieldInteractor : IGameFieldInteractor
    {
        private IMonoBehaviourPool<IGameFieldCellDestroyParticlesView> _gameFieldCellDestroyParticlesPool;
        private IMonoBehaviourPool<IGameFieldCellExplosionParticlesView> _gameFieldCellExplosionParticlesPool;
        private IGameContainer _gameContainer;

        private GameFieldConfig _gameFieldConfig;

        private IGameFieldGridView _gameFieldGridView;
        private int _activeCellCount;
        private int _totalCellCount;

        ///  <inheritdoc />
        public IGameFieldGridView GameFieldGridView => _gameFieldGridView;

        ///  <inheritdoc />
        public int ActiveCellCount => _activeCellCount;

        ///  <inheritdoc />
        public int TotalCellCount => _totalCellCount;

        ///  <inheritdoc />
        public Action OnGameFieldCellDestroy { get; set; }

        ///  <inheritdoc />
        public Action<IGameFieldCellView> OnGameFieldCellDestroyExtended { get; set; }

        ///  <inheritdoc />
        public Action OnAllGameFieldCellDestroy { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public GameFieldInteractor(IMonoBehaviourPool<IGameFieldCellDestroyParticlesView> gameFieldCellDestroyParticlesPool,
            IMonoBehaviourPool<IGameFieldCellExplosionParticlesView> gameFieldCellExplosionParticlesPool,
            IGameContainer gameContainer,
            GameFieldConfig gameFieldConfig)
        {
            _gameFieldCellDestroyParticlesPool = gameFieldCellDestroyParticlesPool;
            _gameFieldCellExplosionParticlesPool = gameFieldCellExplosionParticlesPool;
            _gameContainer = gameContainer;

            _gameFieldConfig = gameFieldConfig;

            _gameFieldCellDestroyParticlesPool.SetPrefab(_gameFieldConfig.GameFieldCellDestroyParticlesPrefab);
            _gameFieldCellDestroyParticlesPool.SetContainer(_gameContainer.CoreContainer);
            _gameFieldCellExplosionParticlesPool.SetPrefab(_gameFieldConfig.GameFieldCellExplosionParticlesPrefab);
            _gameFieldCellExplosionParticlesPool.SetContainer(_gameContainer.CoreContainer);
        }

        ///  <inheritdoc />
        public void Restart()
        {
            _totalCellCount = _gameFieldGridView.CellViews.Count;
            _activeCellCount = _totalCellCount;
        }

        ///  <inheritdoc />
        public void SetGameFieldGrid(IGameFieldGridView gameFieldGridView)
        {
            _gameFieldGridView = gameFieldGridView;
        }

        ///  <inheritdoc />
        public IGameFieldCellView GetCell(Vector2Int gridPosition)
        {
            var targetCellId = _gameFieldGridView.CellViews.FindIndex(x => x.CellData.GridPosition == gridPosition);
            if(targetCellId < 0)
            {
                return null;
            }

            return _gameFieldGridView.CellViews[targetCellId];
        }

        ///  <inheritdoc />
        public IGameFieldCellView GetCell(int x, int y)
        {
            var targetCellId = _gameFieldGridView.CellViews.FindIndex(c => c.CellData.GridPosition.x == x && c.CellData.GridPosition.y == y);
            if (targetCellId < 0)
            {
                return null;
            }

            return _gameFieldGridView.CellViews[targetCellId];
        }

        ///  <inheritdoc />
        public void SetCellActive(IGameFieldCellView gameFieldCellView, bool value)
        {
            gameFieldCellView.CellData.IsActive = value;
            gameFieldCellView.Transform.gameObject.SetActive(value);
        }

        ///  <inheritdoc />
        public void DestroyCell(IGameFieldCellView gameFieldCellView)
        {
            if (!gameFieldCellView.CellData.IsActive)
            {
                return;
            }

            SetCellActive(gameFieldCellView, false);
            _activeCellCount--;


            OnGameFieldCellDestroy?.Invoke();
            OnGameFieldCellDestroyExtended?.Invoke(gameFieldCellView);
            if (_activeCellCount <= 0)
            {
                OnAllGameFieldCellDestroy?.Invoke();
            }

            if (gameFieldCellView.CellData.IsExplosive)
            {
                CreateGameFieldCellExplosionParticles(gameFieldCellView.Transform.position);
                for (int i = gameFieldCellView.CellData.GridPosition.x - 1; i < gameFieldCellView.CellData.GridPosition.x + 2; i++)
                {
                    for (int j = gameFieldCellView.CellData.GridPosition.y - 1; j < gameFieldCellView.CellData.GridPosition.y + 2; j++)
                    {
                        var nearbyCell = GetCell(i, j);
                        if(nearbyCell == null || nearbyCell == gameFieldCellView)
                        {
                            continue;
                        }

                        DestroyCell(nearbyCell);
                    }
                }
            }
        }

        ///  <inheritdoc />
        public void DestroyCellByBall(IGameFieldCellView gameFieldCellView, Vector3 collisionPosition)
        {
            CreateGameFieldCellDestroyParticles(collisionPosition);
            DestroyCell(gameFieldCellView);
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

        /// <summary>
        /// Создать партиклы уничтожения игровой ячейки
        /// </summary>
        private void CreateGameFieldCellExplosionParticles(Vector3 position)
        {
            var particlesView = _gameFieldCellExplosionParticlesPool.GetFreeElement();
            particlesView.Transform.position = position;
            particlesView.OnParticleStop += DestroyGameFieldCellExplosionParticles;
        }

        /// <summary>
        /// Уничтожить партиклы уничтожения игровой ячейки
        /// </summary>
        /// <param name="particlesView"></param>
        private void DestroyGameFieldCellExplosionParticles(IParticlesView particlesView)
        {
            particlesView.OnParticleStop -= DestroyGameFieldCellExplosionParticles;
            _gameFieldCellExplosionParticlesPool.Free(particlesView as IGameFieldCellExplosionParticlesView);
        }
    }
}