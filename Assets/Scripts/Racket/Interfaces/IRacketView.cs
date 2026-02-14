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
        BoxCollider2D BoxCollider2D { get; }

        /// <summary>
        /// Основной Спрайт рендерер 
        /// </summary>
        SpriteRenderer MainSpriteRenderer { get; }

        /// <summary>
        /// Данные ракетки
        /// </summary>
        RacketData RacketData { get; set; }
    }
}