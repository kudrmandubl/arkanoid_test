using System;
using UnityEngine;

namespace GameField.Data
{
    /// <summary>
    /// Параметры создания игрового поля
    /// </summary>
    [Serializable]
    public class GameFieldCreateParams
    {
        [SerializeField] private int _explosiveCellStep;
        [SerializeField] private Color _startCellColor;
        [SerializeField] private Color _endCellColor;

        /// <summary>
        /// Шаг добавления взрывающейся ячейки
        /// </summary>
        public int ExplosiveCellStep => _explosiveCellStep;

        /// <summary>
        /// Начальный цвет ячейки
        /// </summary>
        public Color StartCellColor => _startCellColor;

        /// <summary>
        /// Конечный цвет ячейки
        /// </summary>
        public Color EndCellColor => _endCellColor;
    }
}
