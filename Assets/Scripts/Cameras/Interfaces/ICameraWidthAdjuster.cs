
namespace Cameras.Interfaces
{
    /// <summary>
    /// Регулятор ширины камеры
    /// </summary>
    public interface ICameraWidthAdjuster
    {
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