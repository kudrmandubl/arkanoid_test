using System.Linq;
using DataStorage.Implementations.Systems;
using DataStorage.Interfaces;
using GameField.Data;
using GameField.Interfaces;
using UnityEngine;

namespace GameField.Implementations.Systems
{
    ///  <inheritdoc />
    public class GameFieldInteractor : IGameFieldInteractor
    {
        private IGameFieldGridView _gameFieldGridView;

        ///  <inheritdoc />
        public IGameFieldGridView GameFieldGridView => _gameFieldGridView;

        /// <summary>
        /// Конструктор
        /// </summary>
        public GameFieldInteractor()
        {
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
        }
    }
}