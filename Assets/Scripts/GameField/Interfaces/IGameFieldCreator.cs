
namespace GameField.Interfaces
{
    /// <summary>
    /// Создатель игрового поля
    /// </summary>
    public interface IGameFieldCreator
    {
        /// <summary>
        /// Создать игровое поле
        /// </summary>
        /// <returns></returns>
        IGameFieldGridView CreateGameField();

        /// <summary>
        /// Установить начальные данные ячеек игрового поля
        /// </summary>
        void SetStartGameFieldCellsData();
    }
}