using Screens.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Screens.Interfaces
{
    /// <summary>
    /// Верхняя панель
    /// </summary>
    public interface ITopPanel
    {
        /// <summary>
        /// Кнопка назад
        /// </summary>
        Button BackButton { get; }
    }
}