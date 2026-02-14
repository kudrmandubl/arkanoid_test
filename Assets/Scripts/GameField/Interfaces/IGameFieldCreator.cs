using GameField.Data;

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
        IGameFieldGridView CreateGameField(GameFieldCreateParams createParams);
    }
}