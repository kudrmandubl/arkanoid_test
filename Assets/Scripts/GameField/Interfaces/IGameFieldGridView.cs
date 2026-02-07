using System.Collections.Generic;
using UnityEngine;

namespace GameField.Interfaces
{
    /// <summary>
    /// Отображение сетка игрового поля
    /// </summary>
    public interface IGameFieldGridView
    {
        /// <summary>
        /// Контейнер ячеек
        /// </summary>
        Transform CellsContainer { get; }

        /// <summary>
        /// Отображения ячеек
        /// </summary>
        List<IGameFieldCellView> CellViews { get; set;}
    }
}