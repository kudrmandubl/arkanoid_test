using System;
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
        /// Количество активных ячеек
        /// </summary>
        int ActiveCellCount { get; }

        /// <summary>
        /// Общее количество ячеек
        /// </summary>
        int TotalCellCount { get; }

        /// <summary>
        /// При уничтожении ячейки
        /// </summary>
        Action OnGameFieldCellDestroy { get; set; }

        /// <summary>
        /// При уничтожении ячейки
        /// </summary>
        Action<IGameFieldCellView> OnGameFieldCellDestroyExtended { get; set; }

        /// <summary>
        /// При уничтожении всех ячеек
        /// </summary>
        Action OnAllGameFieldCellDestroy { get; set; }

        /// <summary>
        /// Рестарт
        /// </summary>
        void Restart();

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
        /// Получить ячейку
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Ячейка</returns>
        IGameFieldCellView GetCell(int x, int y);

        /// <summary>
        /// Установить активность ячейки
        /// </summary>
        /// <param name="gameFieldCellView"></param>
        /// <param name="value"></param>
        void SetCellActive(IGameFieldCellView gameFieldCellView, bool value);

        /// <summary>
        /// Уничтожить ячейку
        /// </summary>
        /// <param name="gameFieldCellView"></param>
        void DestroyCell(IGameFieldCellView gameFieldCellView);

        /// <summary>
        /// Уничтожить ячейку шариком
        /// </summary>
        /// <param name="gameFieldCellView"></param>
        void DestroyCellByBall(IGameFieldCellView gameFieldCellView, Vector3 collisionPoisiton);
    }
}