using System;
using Buffs.Enums;

namespace Buffs.Data
{
    /// <summary>
    /// Данные бафа
    /// </summary>
    [Serializable]
    public class BuffData
    {
        /// <summary>
        /// Тип бафа
        /// </summary>
        public BuffType BuffType;

        /// <summary>
        /// Инт значение
        /// </summary>
        public int IntValue;

        /// <summary>
        /// Флоат значение
        /// </summary>
        public float FloatValue;
    }
}
