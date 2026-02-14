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
        [SerializeField] private GameObject _triesCountRoot;
        [SerializeField] private TextMeshProUGUI _triesCountText;
        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private Button _continueButton;
        [SerializeField] private TextMeshProUGUI _continueButtonText;
        [SerializeField] private Button _toMenuButton;

        ///  <inheritdoc />
        public GameObject TriesCountRoot => _triesCountRoot;

        ///  <inheritdoc />
        public TextMeshProUGUI TriesCountText => _triesCountText;

        ///  <inheritdoc />
        public TextMeshProUGUI ResultText => _resultText;

        ///  <inheritdoc />
        public Button ContinueButton => _continueButton;

        ///  <inheritdoc />
        public TextMeshProUGUI ContinueButtonText => _continueButtonText;

        ///  <inheritdoc />
        public Button ToMenuButton => _toMenuButton;
    }
}
