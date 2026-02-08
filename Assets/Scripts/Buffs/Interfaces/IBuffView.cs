using Buffs.Data;
using Common.Interfaces;
using UnityEngine;

namespace Buffs.Interfaces
{
    /// <summary>
    /// Отображение бафа
    /// </summary>
    public interface IBuffView : IBaseView
    {
        /// <summary>
        /// Физическое тело
        /// </summary>
        Rigidbody2D Rigidbody2D { get; }

        /// <summary>
        /// Основной Спрайт рендерер 
        /// </summary>
        SpriteRenderer MainSpriteRenderer { get; }

        /// <summary>
        /// Данные бафа
        /// </summary>
        BuffData BuffData { get; set; }
    }
}