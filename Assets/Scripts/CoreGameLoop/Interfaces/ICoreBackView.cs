using UnityEngine;

namespace CoreGameLoop.Interfaces
{
    /// <summary>
    /// Отображение фона кора
    /// </summary>
    public interface ICoreBackView
    {
        /// <summary>
        /// Ренедерер спрайта фона
        /// </summary>
        SpriteRenderer BackSpriteRenderer { get; }
    }
}