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
        public int _highScore;

        /// <summary>
        /// Конструктор
        /// </summary>
        public PlayerStorageData()
        {
            _highScore = 0;
        }

        /// <summary>
        /// Очистить
        /// </summary>
        public void Clear()
        {
            _highScore = 0;
        }
    }
}