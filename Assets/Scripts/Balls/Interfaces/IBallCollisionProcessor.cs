using System;
using GameField.Interfaces;

namespace Balls.Interfaces
{
    /// <summary>
    /// Обрабатыватель коллизий шариков
    /// </summary>
    public interface IBallCollisionProcessor
    {
        /// <summary>
        /// При уничтожении ячейки игрового поля
        /// </summary>
        Action<IGameFieldCellView> OnDestroyGameFieldCelLView { get; set; }
    }
}