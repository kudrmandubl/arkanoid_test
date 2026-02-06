using Common.Implementations.Systems;
using Common.Interfaces;
using Zenject;

namespace Common.DI
{
    /// <summary>
    /// DI инстоллер для общего
    /// </summary>
    public class CommonInstaller : MonoInstaller
    {
        /// <summary>
        /// Установить зависимости
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IGameContainer>().To<GameContainer>().AsCached();
            Container.Bind<IMonoBehaviourCycle>()
                .To<SimpleMonoBehaviourCycle>()
                .FromNewComponentOnNewGameObject()
                .AsCached();
            Container.Bind<IMainCamera>().To<MainCamera>().AsCached();
        }
    }
}