using System;

namespace DataStorage.Data
{
    /// <summary>
    /// Хранимые данные игрока
    /// </summary>
    [Serializable]
    public class PlayerStorageData : BaseStorageData
    {
        /// <summary>
        /// Максимальный счёт
        /// </summary>
        public int HighScore;

        /// <summary>
        /// Конструктор
        /// </summary>
        public PlayerStorageData()
        {
            HighScore = 0;
        }

        /// <summary>
        /// Очистить
        /// </summary>
        public void Clear()
        {
            HighScore = 0;
        }
    }
}