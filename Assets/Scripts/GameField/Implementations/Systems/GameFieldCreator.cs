using System.Collections.Generic;
using Common.Interfaces;
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
        public IGameFieldGridView CreateGameField(GameFieldCreateParams createParams)
        {
            if(_gameFieldGridView == null)
            {
                _gameFieldGridView = FirstCreateGameField();
                _gameFieldInteractor.SetGameFieldGrid(_gameFieldGridView);
            }

            ReactivateGameField(createParams);

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
            for (int i = 0; i < _gameFieldConfig.GameFieldSize.x; i++)
            {
                for (int j = 0; j < _gameFieldConfig.GameFieldSize.y; j++)
                {
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

                }
            }

            gameFieldGridView.CellViews = gameFieldCellViews;

            return gameFieldGridView;
        }

        /// <summary>
        /// Реактивировать игрокое поле
        /// </summary>
        private void ReactivateGameField(GameFieldCreateParams createParams)
        {
            var maxDeltaColor = createParams.EndCellColor - createParams.StartCellColor;
            for (int i = 0; i < _gameFieldGridView.CellViews.Count; i++)
            {
                var cellView = _gameFieldGridView.CellViews[i];
                _gameFieldInteractor.SetCellActive(cellView, true);

                var isExplosive = i % createParams.ExplosiveCellStep == 0;
                cellView.CellData.IsExplosive = isExplosive;
                cellView.ExplosiveRoot.SetActive(isExplosive);
                var deltaColorX = maxDeltaColor * cellView.CellData.GridPosition.x / (_gameFieldConfig.GameFieldSize.x - 1);
                var deltaColorY = maxDeltaColor * cellView.CellData.GridPosition.y / (_gameFieldConfig.GameFieldSize.y - 1);
                cellView.MainSpriteRenderer.color = createParams.StartCellColor + (deltaColorX + deltaColorY) * 0.5f;
            }
        }
    }
}