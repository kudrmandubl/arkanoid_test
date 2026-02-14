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
        /// Коллайдер
        /// </summary>
        Collider2D Collider2D { get; }

        /// <summary>
        /// Основной Спрайт рендерер 
        /// </summary>
        SpriteRenderer MainSpriteRenderer { get; }

        /// <summary>
        /// Транcформ для изменения размера
        /// </summary>
        Transform SizableTransform { get; }

        /// <summary>
        /// Корень объекта показывающего взрываемость ячейки
        /// </summary>
        GameObject ExplosiveRoot { get; }

        /// <summary>
        /// Данные ячейки
        /// </summary>
        GameFieldCellData CellData { get; set; }
    }
}