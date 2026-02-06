using CoreGameLoop.Implementations.Systems;
using CoreGameLoop.Interfaces;
using Zenject;

namespace CoreGameLoop.DI
{
    /// <summary>
    /// DI инстоллер для основного игрового цикла
    /// </summary>
    public class CoreGameLoopInstaller : MonoInstaller
    {
        /// <summary>
        /// Установить зависимости
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<ICoreGameLoopSystem>().To<CoreGameLoopSystem>().AsCached();
        }
    }
}