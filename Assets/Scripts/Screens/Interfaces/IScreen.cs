using Common.Interfaces;
using UnityEngine;

namespace Screens.Interfaces
{
    /// <summary>
    /// Экран
    /// </summary>
    public interface IScreen : IBaseView
    {
        /// <summary>
        /// РектТрансформ
        /// </summary>
        RectTransform RectTransform { get; }

        /// <summary>
        /// Верхняя панель
        /// </summary>
        ITopPanel TopPanel { get; }

        /// <summary>
        /// Установить активность
        /// </summary>
        /// <param name="value">Флаг активности</param>
        void SetActive(bool value);
    }
}