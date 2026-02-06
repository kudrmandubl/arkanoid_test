using UnityEngine;

namespace Common.Interfaces
{
    /// <summary>
    /// Контейнер игры
    /// </summary>
    public interface IGameContainer
    {
        /// <summary>
        /// Контайнер кора
        /// </summary>
        Transform CoreContainer { get; }

        /// <summary>
        /// Контайнер меты
        /// </summary>
        Transform MetaContainer { get; }

        /// <summary>
        /// Общий меты
        /// </summary>
        Transform CommonContainer { get; }
    }
}
