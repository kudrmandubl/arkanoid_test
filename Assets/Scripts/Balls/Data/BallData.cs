using System;
using UnityEngine;

namespace Balls.Data
{
    /// <summary>
    /// Данные шарика
    /// </summary>
    [Serializable]
    public class BallData
    {
        /// <summary>
        /// Размер
        /// </summary>
        public Vector2 Size;

        /// <summary>
        /// Направление
        /// </summary>
        public Vector3 Direction;

        /// <summary>
        /// Скорость
        /// </summary>
        public float Speed;

        /// <summary>
        /// Шаг скорости
        /// </summary>
        public float SpeedStep;

        /// <summary>
        /// Флаг возможность сколайдиться
        /// </summary>
        public bool IsCanCollide;
    }
}
