using Screens.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Screens.UI
{
    ///  <inheritdoc />
    public class TopPanel : MonoBehaviour, ITopPanel
    {
        [SerializeField] private Button _backButton;

        ///  <inheritdoc />
        public Button BackButton => _backButton;
    }
}