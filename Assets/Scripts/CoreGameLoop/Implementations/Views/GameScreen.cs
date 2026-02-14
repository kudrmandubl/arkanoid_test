using CoreGameLoop.Interfaces;
using Screens.Implementations.Views;
using TMPro;
using UnityEngine;

namespace CoreGameLoop.Implementations.Views
{
    ///  <inheritdoc cref="IGameScreen" />
    public class GameScreen : BaseScreen, IGameScreen
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _triesCountText;

        ///  <inheritdoc />
        public TextMeshProUGUI LevelText => _levelText;

        ///  <inheritdoc />
        public TextMeshProUGUI ScoreText => _scoreText;

        ///  <inheritdoc />
        public TextMeshProUGUI TriesCountText => _triesCountText;
    }
}
