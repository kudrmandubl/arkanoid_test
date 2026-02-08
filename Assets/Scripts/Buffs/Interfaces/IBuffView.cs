using System;
using Buffs.Data;
using Buffs.Implementations.Views;
using Common.Interfaces;
using Racket.Interfaces;
using UnityEngine;

namespace Buffs.Interfaces
{
    /// <summary>
    /// Отображение бафа
    /// </summary>
    public interface IBuffView : IBaseView
    {
        /// <summary>
        /// При вхождении в триггер ракетки
        /// </summary>
        Action<IBuffView, IRacketView> OnRacketTriggerEnter { get; set; }

        /// <summary>
        /// Физическое тело
        /// </summary>
        Rigidbody2D Rigidbody2D { get; }

        /// <summary>
        /// Основной Спрайт рендерер 
        /// </summary>
        SpriteRenderer MainSpriteRenderer { get; }

        /// <summary>
        /// Вариант отображения бафа
        /// </summary>
        BuffViewVariant[] BuffViewVariants { get; }

        /// <summary>
        /// Данные бафа
        /// </summary>
        BuffData BuffData { get; set; }
    }
}