
namespace CoreGameLoop.Interfaces
{
    /// <summary>
    /// Система основного игрового цикла
    /// </summary>
    public interface ICoreGameLoopSystem
    {
        /// <summary>
        /// Инициализировать
        /// </summary>
        void Initialize();

        /// <summary>
        /// Начать игровой цикл
        /// </summary>
        void StartGameLoop();

        /// <summary>
        /// Остановить игрового цикла
        /// </summary>
        void StopGameLoop();
    }
}