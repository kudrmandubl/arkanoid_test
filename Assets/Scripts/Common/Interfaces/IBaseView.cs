
using UnityEngine;

namespace Common.Interfaces
{
    /// <summary>
    /// Базовое отображение
    /// </summary>
    public interface IBaseView
    {
        /// <summary>
        /// Трансформ
        /// </summary>
        Transform Transform { get; }
    }
}