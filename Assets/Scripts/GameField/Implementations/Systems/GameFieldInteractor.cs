using System.Linq;
using DataStorage.Data;
using DataStorage.Interfaces;
using GameField.Data;
using GameField.Interfaces;
using UnityEngine;

namespace GameField.Implementations.Systems
{
    ///  <inheritdoc />
    public class GameFieldInteractor : IGameFieldInteractor
    {
        private IDataStorageSystem _dataStorageSystem;

        private IGameFieldGridView _gameFieldGridView;

        ///  <inheritdoc />
        public IGameFieldGridView GameFieldGridView => _gameFieldGridView;

        /// <summary>
        /// Конструктор
        /// </summary>
        public GameFieldInteractor(IDataStorageSystem dataStorageSystem)
        {
            _dataStorageSystem = dataStorageSystem;
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
        public void SetCellActive(IGameFieldCellView gameFieldCellView, bool value, bool needSave)
        {
            gameFieldCellView.CellData.IsActive = value;

            if (!needSave)
            {
                return;
            }

            var gameFieldStorageData = _dataStorageSystem.GetStorageData<GameFieldStorageData>();
            var cellStorageData = gameFieldStorageData.GameFieldCellsData.FirstOrDefault(x => x.GridPosition == gameFieldCellView.CellData.GridPosition);
            if (cellStorageData == null)
            {
                cellStorageData = new GameFieldCellData();
                gameFieldStorageData.GameFieldCellsData.Add(cellStorageData);
            }

            cellStorageData.IsActive = value;
            _dataStorageSystem.Save<GameFieldStorageData>();
        }
    }
}