using MainMenu.Implementations.Systems;
using MainMenu.Interfaces;
using Zenject;

namespace MainMenu.DI
{
    /// <summary>
    /// DI инстоллер для меню
    /// </summary>
    public class MenuLoopInstaller : MonoInstaller
    {
        /// <summary>
        /// Установить зависимости
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IMainMenuParallax>().To<MainMenuParallax>().AsCached();
            Container.Bind<IMainMenu>().To<MainMenu.Implementations.Systems.MainMenu>().AsCached();
        }
    }
}