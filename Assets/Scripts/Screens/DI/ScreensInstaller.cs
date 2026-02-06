using Screens.Implementations.Systems;
using Screens.Interfaces;
using Zenject;

namespace Screens.DI
{
    /// <summary>
    /// DI инстоллер для экранов
    /// </summary>
    public class ScreensInstaller : MonoInstaller
    {
        /// <summary>
        /// Установить зависимости
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IScreenSystem>().To<ScreenSystem>().AsCached();
        }

    }
}