using Screens.Interfaces;
using UnityEngine.UI;

namespace CoreGameLoop.Interfaces
{
    /// <summary>
    /// Экран паузы
    /// </summary>
    public interface IPauseScreen : IScreen
    {
        /// <summary>
        /// Кнопка продолжить
        /// </summary>
        Button ContinueButton { get; }

        /// <summary>
        /// Кнопка в меню
        /// </summary>
        Button ToMenuButton { get; }
    }
}