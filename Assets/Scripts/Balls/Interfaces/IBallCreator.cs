
using System;

namespace Balls.Interfaces
{
    /// <summary>
    /// Создатель шариков
    /// </summary>
    public interface IBallCreator
    {
        /// <summary>
        /// При создании шарика
        /// </summary>
        Action<IBallView> OnCreateBall { get; set; }

        /// <summary>
        /// При уничтожении шарика
        /// </summary>
        Action<IBallView> OnDestroyBall { get; set; }

        /// <summary>
        /// Создать шар
        /// </summary>
        void CreateBall();

        /// <summary>
        /// Уничтожить шарик
        /// </summary>
        /// <param name="ballView"></param>
        void DestroyBall(IBallView ballView);

        /// <summary>
        /// Уничтожить все шарики
        /// </summary>
        void DestroyAllBalls();
    }
}