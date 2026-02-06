using Screens.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Interfaces
{
    /// <summary>
    /// Экран основного меню
    /// </summary>
    public interface IMainMenuScreen : IScreen
    {
        /// <summary>
        /// Кнопка начала игры
        /// </summary>
        Button PlayButton { get; }

        /// <summary>
        /// Текст лучшего результата
        /// </summary>
        TextMeshProUGUI HighScoreText { get; }

        /// <summary>
        /// Передняя часть
        /// </summary>
        Transform FrontPart { get; }

        /// <summary>
        /// Задняя часть
        /// </summary>
        Transform BackPart { get; }
    }
}