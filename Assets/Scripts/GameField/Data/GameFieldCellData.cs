using System;
using UnityEngine;

namespace GameField.Data
{
    /// <summary>
    /// Данные ячейки игрового поля
    /// </summary>
    [Serializable]
    public class GameFieldCellData
    {
        /// <summary>
        /// Позиция в сетке
        /// </summary>
        public Vector2Int GridPosition;

        /// <summary>
        /// Флаг активности
        /// </summary>
        public bool IsActive;

        /// <summary>
        /// Флаг взрываемости
        /// </summary>
        public bool IsExplosive;
    }
}
