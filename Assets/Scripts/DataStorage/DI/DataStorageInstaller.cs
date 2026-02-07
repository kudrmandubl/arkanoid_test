using DataStorage.Implementations.Systems;
using DataStorage.Interfaces;
using Zenject;

namespace DataStorage.DI
{
    /// <summary>
    /// DI инстоллер для хранилища данных
    /// </summary>
    public class DataStorageInstaller : MonoInstaller
    {
        /// <summary>
        /// Установить зависимости
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IDataStorageSystem>().To<DataStorageSystem>().AsCached();
        }
    }
}