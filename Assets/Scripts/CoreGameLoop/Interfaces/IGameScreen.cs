using Screens.Interfaces;
using TMPro;

namespace CoreGameLoop.Interfaces
{
    /// <summary>
    /// Экран игры
    /// </summary>
    public interface IGameScreen : IScreen
    {
        /// <summary>
        /// Текст уровня
        /// </summary>
        TextMeshProUGUI LevelText { get; }

        /// <summary>
        /// Текст очков
        /// </summary>
        TextMeshProUGUI ScoreText { get; }

        /// <summary>
        /// Текст количества попыток
        /// </summary>
        TextMeshProUGUI TriesCountText { get; }
    }
}