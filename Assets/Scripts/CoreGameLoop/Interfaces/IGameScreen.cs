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
        /// Кнопка возврата из игры
        /// </summary>
        TextMeshProUGUI ScoreText { get; }
    }
}