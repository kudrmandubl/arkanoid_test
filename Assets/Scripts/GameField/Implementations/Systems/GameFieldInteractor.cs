using System;
using GameField.Interfaces;
using UnityEngine;

namespace GameField.Implementations.Systems
{
    ///  <inheritdoc />
    public class GameFieldInteractor : IGameFieldInteractor
    {
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
        public Action OnAllGameFieldCellDestroy { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public GameFieldInteractor()
        {
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
                Debug.LogError($"Не удалось получить ячейку с позицией в сетке {gridPosition}");
                return null;
            }

            return _gameFieldGridView.CellViews[targetCellId];
        }


        ///  <inheritdoc />
        public void SetCellActive(IGameFieldCellView gameFieldCellView, bool value)
        {
            gameFieldCellView.CellData.IsActive = value;
            gameFieldCellView.Transform.gameObject.SetActive(value);
            if (!value)
            {
                _activeCellCount--;
                OnGameFieldCellDestroy?.Invoke();
                if (_activeCellCount <= 0)
                {
                    OnAllGameFieldCellDestroy?.Invoke();
                }
            }
        }
    }
}