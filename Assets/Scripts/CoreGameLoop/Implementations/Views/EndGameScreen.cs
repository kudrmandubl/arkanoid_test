using CoreGameLoop.Interfaces;
using Screens.Implementations.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CoreGameLoop.Implementations.Views
{
    ///  <inheritdoc cref="IEndGameScreen" />
    public class EndGameScreen : BaseScreen, IEndGameScreen
    {
        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _toMenuButton;

        ///  <inheritdoc />
        public TextMeshProUGUI ResultText => _resultText;

        ///  <inheritdoc />
        public Button ContinueButton => _continueButton;

        ///  <inheritdoc />
        public Button ToMenuButton => _toMenuButton;
    }
}
