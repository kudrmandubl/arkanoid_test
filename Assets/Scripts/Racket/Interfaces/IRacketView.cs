using Common.Interfaces;
using Racket.Data;
using UnityEngine;

namespace Racket.Interfaces
{
    /// <summary>
    /// Отображение ячейки игрового поля
    /// </summary>
    public interface IRacketView : IBaseView
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
        /// Данные ракетки
        /// </summary>
        RacketData RacketData { get; set; }
    }
}