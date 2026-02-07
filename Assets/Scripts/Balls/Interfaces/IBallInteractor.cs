using System.Collections.Generic;

namespace Balls.Interfaces
{
    /// <summary>
    /// Точка взаимодействия с шариками
    /// </summary>
    public interface IBallInteractor
    {
        /// <summary>
        /// Отображение ракетки
        /// </summary>
        List<IBallView> BallViews { get; }

        /// <summary>
        /// Добавить шарик
        /// </summary>
        void AddBall(IBallView ballView);

        /// <summary>
        /// Убрать шарик
        /// </summary>
        void RemoveBall(IBallView ballView);
    }
}