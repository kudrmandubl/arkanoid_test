using MenuLoop.Implementations.Systems;
using MenuLoop.Interfaces;
using Zenject;

namespace MenuLoop.DI
{
    /// <summary>
    /// DI инстоллер для цикла меню
    /// </summary>
    public class MenuLoopInstaller : MonoInstaller
    {
        /// <summary>
        /// Установить зависимости
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IMenuLoopSystem>().To<MenuLoopSystem>().AsCached().NonLazy();
        }
    }
}