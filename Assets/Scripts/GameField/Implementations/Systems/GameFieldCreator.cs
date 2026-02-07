using System.Collections.Generic;
using System.Linq;
using Common.Interfaces;
using DataStorage.Data;
using DataStorage.Interfaces;
using GameField.Configs;
using GameField.Data;
using GameField.Implementations.Views;
using GameField.Interfaces;
using UnityEngine;

namespace GameField.Implementations.Systems
{
    ///  <inheritdoc />
    public class GameFieldCreator : IGameFieldCreator
    {
        private IGameContainer _gameContainer;
        private IGameFieldInteractor _gameFieldCreatorInteractor;
        private IDataStorageSystem _dataStorageSystem;

        private GameFieldConfig _gameFieldConfig;

        private IGameFieldGridView _gameFieldGridView;

        /// <summary>
        /// Конструктор
        /// </summary>
        public GameFieldCreator(IGameContainer gameContainer,
            IGameFieldInteractor gameFieldCreatorInteractor,
            IDataStorageSystem dataStorageSystem,
            GameFieldConfig gameFieldConfig)
        {
            _gameContainer = gameContainer;
            _gameFieldCreatorInteractor = gameFieldCreatorInteractor;
            _dataStorageSystem = dataStorageSystem;

            _gameFieldConfig = gameFieldConfig;
        }

        ///  <inheritdoc />
        public IGameFieldGridView CreateGameField()
        {
            if(_gameFieldGridView != null)
            {
                return _gameFieldGridView;
            }

            var gameFieldGridView = GameObject.Instantiate(_gameFieldConfig.GameFieldGridViewPrefab, _gameContainer.CoreContainer);
            gameFieldGridView.Transform.localPosition = _gameFieldConfig.GameFieldGridPosition;
            
            var gameFieldCellViews = new List<IGameFieldCellView>();
            for (int i = 0; i < _gameFieldConfig.GameFieldSize.x; i++)
            {
                for (int j = 0; j < _gameFieldConfig.GameFieldSize.y; j++)
                {
                    var gameFieldCellView = GameObject.Instantiate(_gameFieldConfig.GameFieldCellViewPrefab, gameFieldGridView.CellsContainer);
                    gameFieldCellViews.Add(gameFieldCellView);

                    var cellData = new GameFieldCellData();
                    gameFieldCellView.CellData = cellData;

                    cellData.GridPosition = new Vector2Int(i, j);
                    _gameFieldCreatorInteractor.SetCellActive(gameFieldCellView, true, false);

                    var positionX = (i + 1) * _gameFieldConfig.GameFieldCellSize.x 
                        - _gameFieldConfig.GameFieldSize.x * _gameFieldConfig.GameFieldCellSize.x * 0.5f
                        - _gameFieldConfig.GameFieldCellSize.x * 0.5f;
                    var positionY = (j + 1) * _gameFieldConfig.GameFieldCellSize.y
                        - _gameFieldConfig.GameFieldSize.y * _gameFieldConfig.GameFieldCellSize.y * 0.5f
                        - _gameFieldConfig.GameFieldCellSize.y * 0.5f;
                    var position = new Vector3(positionX, positionY, 0);
                    gameFieldCellView.Transform.localPosition = position;

                    gameFieldCellView.MainSpriteRenderer.transform.localScale = _gameFieldConfig.GameFieldCellSize - Vector3.one * _gameFieldConfig.GameFieldCellPadding;
                }
            }

            _gameFieldGridView = gameFieldGridView;
            _gameFieldGridView.CellViews = gameFieldCellViews;

            _gameFieldCreatorInteractor.SetGameFieldGrid(gameFieldGridView);
            return _gameFieldGridView;
        }

        ///  <inheritdoc />
        public void SetStartGameFieldCellsData()
        {
            var gameFieldStorageData = _dataStorageSystem.GetStorageData<GameFieldStorageData>();

            // загрузка данных из прошлой сессии
            // тут же если первый запуск уровня добавить выставление начального паттерна распределения ячеек
            for (int i = 0; i < gameFieldStorageData.GameFieldCellsData.Count; i++)
            {
                var cellStorageData = gameFieldStorageData.GameFieldCellsData[i];
                var targetCellView = _gameFieldGridView.CellViews.FirstOrDefault(x => x.CellData.GridPosition == cellStorageData.GridPosition);

                _gameFieldCreatorInteractor.SetCellActive(targetCellView, cellStorageData.IsActive, false);
            }

            _dataStorageSystem.Save<GameFieldStorageData>();
        }
    }
}