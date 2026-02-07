using CoreGameLoop.Interfaces;
using Screens.Implementations.Views;
using UnityEngine;
using UnityEngine.UI;

namespace CoreGameLoop.Implementations.Views
{
    ///  <inheritdoc cref="IPauseScreen" />
    public class PauseScreen : BaseScreen, IPauseScreen
    {
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _toMenuButton;

        ///  <inheritdoc />
        public Button ContinueButton => _continueButton;

        ///  <inheritdoc />
        public Button ToMenuButton => _toMenuButton;
    }
}
