
namespace Racket.Interfaces
{
    /// <summary>
    /// Система ракетки
    /// </summary>
    public interface IRacketSystem
    {
        /// <summary>
        /// Создать ракетку
        /// </summary>
        void CreateRacket();

        /// <summary>
        /// Установить активность управления
        /// </summary>
        /// <param name="value"></param>
        void SetControlActive(bool value);
    }
}