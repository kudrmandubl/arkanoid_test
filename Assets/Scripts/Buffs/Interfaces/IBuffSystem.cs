
using System;

namespace Buffs.Interfaces
{
    /// <summary>
    /// Система бафов
    /// </summary>
    public interface IBuffSystem
    {
        /// <summary>
        /// При применении бафа добавления жизней
        /// </summary>
        Action<int> OnApplyAddLivesBuff { get; set; }

        /// <summary>
        /// Инициализация
        /// </summary>
        void Initialize();

        /// <summary>
        /// Очистить
        /// </summary>
        void Clear();

        /// <summary>
        /// Установить активность
        /// </summary>
        /// <param name="value"></param>
        void SetActive(bool value);
    }
}