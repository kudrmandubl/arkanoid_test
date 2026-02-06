using Common.Utils;
using Screens.Interfaces;
using Screens.UI;
using UnityEngine;

namespace Screens.Implementations.Views
{
    /// <summary>
    /// Базовый экран
    /// </summary>
    public class BaseScreen : CachedMonoBehaviour
    {
        [SerializeField] private TopPanel _topPanel;

        private RectTransform _rectTransform;

        /// <summary>
        /// РектТрансформ
        /// </summary>
        public RectTransform RectTransform 
        {
            get
            {
                if (!_rectTransform)
                {
                    _rectTransform = GetComponent<RectTransform>();
                }
                return _rectTransform;
            }
        }

        /// <summary>
        /// Верхняя панель
        /// </summary>
        public ITopPanel TopPanel => _topPanel;

        /// <summary>
        /// Установить активность
        /// </summary>
        /// <param name="value"></param>
        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}
