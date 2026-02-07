using Racket.Implementations.Systems;
using Racket.Interfaces;
using Zenject;

namespace Racket.DI
{
    /// <summary>
    /// DI инстоллер для игрового поля
    /// </summary>
    public class RacketInstaller : MonoInstaller
    {
        /// <summary>
        /// Установить зависимости
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IRacketInteractor>().To<RacketInteractor>().AsCached();
            Container.Bind<IRacketSystem>().To<RacketSystem>().AsCached();
        }
    }
}