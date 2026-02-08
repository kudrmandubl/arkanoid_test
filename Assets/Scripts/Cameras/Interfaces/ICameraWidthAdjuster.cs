
using System;

namespace Cameras.Interfaces
{
    /// <summary>
    /// Регулятор ширины камеры
    /// </summary>
    public interface ICameraWidthAdjuster
    {
        /// <summary>
        /// При изменении размера камеры
        /// </summary>
        Action OnCameraSizeChange { get; set; }

        /// <summary>
        /// Инициализировать
        /// </summary>
        void Initialize();

        /// <summary>
        /// Обновить размер камеры
        /// </summary>
        void UpdateCameraSize();
    }
}