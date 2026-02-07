using DataStorage.Data;
using Cysharp.Threading.Tasks;

namespace DataStorage.Interfaces
{
    /// <summary>
    /// Система хранилища данных
    /// </summary>
    public interface IDataStorageSystem
    {
        /// <summary>
        /// Сохранить
        /// </summary>
        /// <typeparam name="T">Тип сохраняемых данных</typeparam>
        void Save<T>() where T : BaseStorageData;

        /// <summary>
        /// Загрузить
        /// </summary>
        UniTask LoadAsync();

        /// <summary>
        /// Получить хранимые данные
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <returns>Хранимые данные</returns>
        T GetStorageData<T>() where T : BaseStorageData;

        /// <summary>
        /// Очистить все данные
        /// </summary>
        void ClearAll();
    }
}