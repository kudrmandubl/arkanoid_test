using System;
using GameField.Data;
using UnityEngine;

namespace CoreGameLoop.Configs
{
    /// <summary>
    /// Данные конфига уровня
    /// </summary>
    [Serializable]
    public class LevelConfigData
    {
        [SerializeField] private GameFieldCreateParams _gameFieldCreateParams;

        /// <summary>
        /// Параметры создания игрового поля
        /// </summary>
        public GameFieldCreateParams GameFieldCreateParams => _gameFieldCreateParams;
    }
}
