using Common.Interfaces;
using GameField.Data;
using UnityEngine;

namespace GameField.Interfaces
{
    /// <summary>
    /// Отображение ячейки игрового поля
    /// </summary>
    public interface IGameFieldCellView : IBaseView
    {
        /// <summary>
        /// Основной Спрайт рендерер 
        /// </summary>
        SpriteRenderer MainSpriteRenderer { get; }

        /// <summary>
        /// Данные ячейки
        /// </summary>
        GameFieldCellData CellData { get; set; }
    }
}