using System;
using Balls.Data;
using Common.Interfaces;
using GameField.Interfaces;
using Racket.Interfaces;
using UnityEngine;

namespace Balls.Interfaces
{
    /// <summary>
    /// Отображение шарика
    /// </summary>
    public interface IBallView : IBaseView
    {
        /// <summary>
        /// При вхождении в триггер ячейки игрового поля
        /// </summary>
        Action<IBallView, IGameFieldCellView, Collider2D> OnGameFieldCellTriggerEnter { get; set; }

        /// <summary>
        /// При вхождении в триггер ракетки
        /// </summary>
        Action<IBallView, IRacketView, Collider2D> OnRacketTriggerEnter { get; set; }

        /// <summary>
        /// Основной Спрайт рендерер 
        /// </summary>
        SpriteRenderer MainSpriteRenderer { get; }

        /// <summary>
        /// Транcформ для изменения размера
        /// </summary>
        Transform SizableTransform { get; }

        /// <summary>
        /// Данные шарика
        /// </summary>
        BallData BallData { get; set; }
    }
}