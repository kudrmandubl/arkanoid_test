using Balls.Data;
using Common.Interfaces;
using UnityEngine;

namespace Balls.Interfaces
{
    /// <summary>
    /// Отображение шарика
    /// </summary>
    public interface IBallView : IBaseView
    {
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