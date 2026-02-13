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
        Action<IBallView, IGameFieldCellView> OnGameFieldCellTriggerEnter { get; set; }

        /// <summary>
        /// При вхождении в триггер ракетки
        /// </summary>
        Action<IBallView, IRacketView> OnRacketTriggerEnter { get; set; }

        /// <summary>
        /// Физическое тело
        /// </summary>
        Rigidbody2D Rigidbody2D { get; }

        /// <summary>
        /// Основной Спрайт рендерер 
        /// </summary>
        SpriteRenderer MainSpriteRenderer { get; }

        /// <summary>
        /// Транcформ для изменения размера
        /// </summary>
        Transform SizableTransform { get; }

        /// <summary>
        /// Рендерер следа
        /// </summary>
        TrailRenderer TrailRenderer { get; }

        /// <summary>
        /// Данные шарика
        /// </summary>
        BallData BallData { get; set; }
    }
}