using MainMenu.Interfaces;
using Screens.Implementations.Views;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Implementations.Views
{
    ///  <inheritdoc cref="IMainMenuScreen" />
    public class MainMenuScreen : BaseScreen, IMainMenuScreen
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private TextMeshProUGUI _highScoreText;
        [SerializeField] private Transform _frontPart;
        [SerializeField] private Transform _backPart;

        ///  <inheritdoc />
        public Button PlayButton => _playButton;

        ///  <inheritdoc />
        public TextMeshProUGUI HighScoreText => _highScoreText;

        ///  <inheritdoc />
        public Transform FrontPart => _frontPart;

        ///  <inheritdoc />
        public Transform BackPart => _backPart;
    }
}
