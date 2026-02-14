using Screens.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CoreGameLoop.Interfaces
{
    /// <summary>
    /// Экран завершения игры
    /// </summary>
    public interface IEndGameScreen : IScreen
    {
        /// <summary>
        /// Корень количества попыток
        /// </summary>
        GameObject TriesCountRoot { get; }

        /// <summary>
        /// Текст количества попыток
        /// </summary>
        TextMeshProUGUI TriesCountText { get; }

        /// <summary>
        /// Текст результата
        /// </summary>
        TextMeshProUGUI ResultText { get; }

        /// <summary>
        /// Кнопка продолжить
        /// </summary>
        Button ContinueButton { get; }

        /// <summary>
        /// Текст кнопки продолжить
        /// </summary>
        TextMeshProUGUI ContinueButtonText { get; }

        /// <summary>
        /// Кнопка в меню
        /// </summary>
        Button ToMenuButton { get; }
    }
}