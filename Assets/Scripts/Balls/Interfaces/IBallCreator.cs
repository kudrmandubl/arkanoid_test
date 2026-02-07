
using System;

namespace Balls.Interfaces
{
    /// <summary>
    /// Создатель шариков
    /// </summary>
    public interface IBallCreator
    {
        /// <summary>
        /// При уничтожении шарика
        /// </summary>
        Action OnDestroyBall { get; set; }

        /// <summary>
        /// Создать шар
        /// </summary>
        void CreateBall();

        /// <summary>
        /// Уничтожить шарик
        /// </summary>
        /// <param name="ballView"></param>
        void DestroyBall(IBallView ballView);
    }
}