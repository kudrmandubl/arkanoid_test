
using System;

namespace CoreGameLoop.Interfaces
{
    /// <summary>
    /// Система основного игрового цикла
    /// </summary>
    public interface ICoreGameLoopSystem
    {
        /// <summary>
        /// При возвращении в меню
        /// </summary>
        Action OnBackToMenu { get; set; }

        /// <summary>
        /// Инициализировать
        /// </summary>
        void Initialize();

        /// <summary>
        /// Начать игровой цикл
        /// </summary>
        void StartGameLoop();
    }
}