
namespace MainMenu.Interfaces
{
    /// <summary>
    /// Параллакс основного меню
    /// </summary>
    public interface IMainMenuParallax
    {
        /// <summary>
        /// Инициализировать
        /// </summary>
        void Initialize();

        /// <summary>
        /// Установить активность
        /// </summary>
        /// <param name="value"></param>
        void SetActive(bool value);
    }
}