using CoreGameLoop.Interfaces;
using Screens.Implementations.Views;
using TMPro;
using UnityEngine;

namespace CoreGameLoop.Implementations.Views
{
    ///  <inheritdoc cref="IGameScreen" />
    public class GameScreen : BaseScreen, IGameScreen
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        ///  <inheritdoc />
        public TextMeshProUGUI ScoreText => _scoreText;
    }
}
