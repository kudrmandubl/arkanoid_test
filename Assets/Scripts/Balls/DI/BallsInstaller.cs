using Balls.Data;
using Balls.Implementations.Systems;
using Balls.Implementations.Views;
using Balls.Interfaces;
using Common.Implementations.Systems.Pools;
using Common.Interfaces;
using NUnit.Framework.Interfaces;
using Zenject;

namespace Balls.DI
{
    /// <summary>
    /// DI инстоллер для шариков
    /// </summary>
    public class BallsInstaller : MonoInstaller
    {
        /// <summary>
        /// Установить зависимости
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IMonoBehaviourPool<IBallView>>().To<MonoBehaviourPool<IBallView, BallView>>().AsCached();
            Container.Bind<IPool<BallData>>().To<SimplePool<BallData, BallData>>().AsCached();
            Container.Bind<IBallInteractor>().To<BallInteractor>().AsCached();
            Container.Bind<IBallCreator>().To<BallCreator>().AsCached();
            Container.Bind<IBallMover>().To<BallMover>().AsCached();
            Container.Bind<IBallCollisionProcessor>().To<BallCollisionProcessor>().AsCached();
        }
    }
}