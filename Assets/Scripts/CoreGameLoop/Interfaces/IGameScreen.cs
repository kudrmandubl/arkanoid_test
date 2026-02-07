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
        /// Текст очков
        /// </summary>
        TextMeshProUGUI ScoreText { get; }
    }
}