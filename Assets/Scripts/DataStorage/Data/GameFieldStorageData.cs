using System;
using System.Collections.Generic;
using GameField.Data;

namespace DataStorage.Data
{
    /// <summary>
    /// Хранимые данные игрового поля
    /// </summary>
    [Serializable]
    public class GameFieldStorageData : BaseStorageData
    {
        /// <summary>
        /// Данные ячеек игрового поля
        /// </summary>
        public List<GameFieldCellData> GameFieldCellsData;

        /// <summary>
        /// Конструктор
        /// </summary>
        public GameFieldStorageData()
        {
            GameFieldCellsData = new List<GameFieldCellData>();
        }

        /// <summary>
        /// Очистить
        /// </summary>
        public void Clear()
        {
            GameFieldCellsData.Clear();
        }
    }
}