using Screens.Interfaces;
using TMPro;
using UnityEngine.UI;

namespace CoreGameLoop.Interfaces
{
    /// <summary>
    /// Экран завершения игры
    /// </summary>
    public interface IEndGameScreen : IScreen
    {
        /// <summary>
        /// Текст результата
        /// </summary>
        TextMeshProUGUI ResultText { get; }

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