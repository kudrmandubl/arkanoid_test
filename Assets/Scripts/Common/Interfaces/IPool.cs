
namespace Common.Interfaces
{
    /// <summary>
    /// Пул
    /// </summary>
    /// <typeparam name="T">Тип объектов в пуле</typeparam>
    public interface IPool<T> where T : class
    {
        /// <summary>
        /// Получить свободный элемента
        /// </summary>
        /// <returns>Свободный элемент</returns>
        T GetFreeElement();

        /// <summary>
        /// Освободить элемент
        /// </summary>
        /// <param name="element">Элемент</param>
        void Free(T element);
    }
}