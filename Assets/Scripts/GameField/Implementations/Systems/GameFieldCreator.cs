using System.Collections.Generic;
using Common.Interfaces;
using DataStorage.Interfaces;
using GameField.Configs;
using GameField.Data;
using GameField.Interfaces;
using UnityEngine;

namespace GameField.Implementations.Systems
{
    ///  <inheritdoc />
    public class GameFieldCreator : IGameFieldCreator
    {
        private IGameContainer _gameContainer;
        private IGameFieldInteractor _gameFieldInteractor;

        private GameFieldConfig _gameFieldConfig;

        private IGameFieldGridView _gameFieldGridView;

        /// <summary>
        /// Конструктор
        /// </summary>
        public GameFieldCreator(IGameContainer gameContainer,
            IGameFieldInteractor gameFieldInteractor,
            GameFieldConfig gameFieldConfig)
        {
            _gameContainer = gameContainer;
            _gameFieldInteractor = gameFieldInteractor;

            _gameFieldConfig = gameFieldConfig;
        }

        ///  <inheritdoc />
        public IGameFieldGridView CreateGameField()
        {
            if(_gameFieldGridView == null)
            {
                _gameFieldGridView = FirstCreateGameField();
                _gameFieldInteractor.SetGameFieldGrid(_gameFieldGridView);
            }
            else
            {
                ReactivateGameField();
            }

            return _gameFieldGridView;
        }

        /// <summary>
        /// Первое создание игрового поля
        /// </summary>
        /// <returns></returns>
        private IGameFieldGridView FirstCreateGameField()
        {
            var gameFieldGridView = GameObject.Instantiate(_gameFieldConfig.GameFieldGridViewPrefab, _gameContainer.CoreContainer);
            gameFieldGridView.Transform.localPosition = _gameFieldConfig.GameFieldGridPosition;

            var gameFieldCellViews = new List<IGameFieldCellView>();
            var maxDeltaColor = _gameFieldConfig.EndCellColor - _gameFieldConfig.StartCellColor;
            for (int i = 0; i < _gameFieldConfig.GameFieldSize.x; i++)
            {
                var deltaColorX = maxDeltaColor * i / (_gameFieldConfig.GameFieldSize.x - 1);
                for (int j = 0; j < _gameFieldConfig.GameFieldSize.y; j++)
                {
                    var deltaColorY = maxDeltaColor * j / (_gameFieldConfig.GameFieldSize.y - 1);

                    var gameFieldCellView = GameObject.Instantiate(_gameFieldConfig.GameFieldCellViewPrefab, gameFieldGridView.CellsContainer);
                    gameFieldCellViews.Add(gameFieldCellView);

                    var cellData = new GameFieldCellData();
                    gameFieldCellView.CellData = cellData;

                    cellData.GridPosition = new Vector2Int(i, j);

                    var positionX = (i + 1) * _gameFieldConfig.GameFieldCellSize.x
                        - _gameFieldConfig.GameFieldSize.x * _gameFieldConfig.GameFieldCellSize.x * 0.5f
                        - _gameFieldConfig.GameFieldCellSize.x * 0.5f;
                    var positionY = (j + 1) * _gameFieldConfig.GameFieldCellSize.y
                        - _gameFieldConfig.GameFieldSize.y * _gameFieldConfig.GameFieldCellSize.y * 0.5f
                        - _gameFieldConfig.GameFieldCellSize.y * 0.5f;
                    var position = new Vector3(positionX, positionY, 0);
                    gameFieldCellView.Transform.localPosition = position;

                    gameFieldCellView.SizableTransform.localScale = Vector2.one * _gameFieldConfig.GameFieldCellSize - Vector2.one * _gameFieldConfig.GameFieldCellPadding;

                    gameFieldCellView.MainSpriteRenderer.color = _gameFieldConfig.StartCellColor + (deltaColorX + deltaColorY) * 0.5f;
                }
            }

            gameFieldGridView.CellViews = gameFieldCellViews;

            return gameFieldGridView;
        }

        /// <summary>
        /// Реактивировать игрокое поле
        /// </summary>
        private void ReactivateGameField()
        {
            for (int i = 0; i < _gameFieldGridView.CellViews.Count; i++)
            {
                _gameFieldInteractor.SetCellActive(_gameFieldGridView.CellViews[i], true);
            }
        }
    }
}