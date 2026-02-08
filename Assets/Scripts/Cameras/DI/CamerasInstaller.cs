using Cameras.Implementations.Systems;
using Cameras.Interfaces;
using Zenject;

namespace Cameras.DI
{
    /// <summary>
    /// DI инстоллер для камер
    /// </summary>
    public class CamerasInstaller : MonoInstaller
    {
        /// <summary>
        /// Установить зависимости
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<ICameraWidthAdjuster>().To<CameraWidthAdjuster>().AsCached();
        }
    }
}