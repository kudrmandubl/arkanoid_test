
using UnityEngine;

namespace GameField.Interfaces
{
    /// <summary>
    /// Точка взаимодействия с игровым полем
    /// </summary>
    public interface IGameFieldInteractor
    {
        /// <summary>
        /// Отображение сетки игрового поля
        /// </summary>
        IGameFieldGridView GameFieldGridView { get; }

        /// <summary>
        /// Установить сетку игрового поля
        /// </summary>
        /// <param name="gameFieldGridView">Сетка игрового поля</param>
        void SetGameFieldGrid(IGameFieldGridView gameFieldGridView);

        /// <summary>
        /// Получить ячейку
        /// </summary>
        /// /// <param name="gridPosition">Позиция в сетке</param>
        /// <returns>Ячейка</returns>
        IGameFieldCellView GetCell(Vector2Int gridPosition);

        /// <summary>
        /// Установить активность ячейки
        /// </summary>
        /// <param name="gameFieldCellView"></param>
        /// <param name="value"></param>
        void SetCellActive(IGameFieldCellView gameFieldCellView, bool value, bool needSave);
    }
}