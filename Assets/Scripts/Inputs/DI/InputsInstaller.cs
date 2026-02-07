using Inputs.Implementations.Systems;
using Inputs.Interfaces;
using Zenject;

namespace Inputs.DI
{
    /// <summary>
    /// DI инстоллер для ввода
    /// </summary>
    public class InputsInstaller : MonoInstaller
    {
        /// <summary>
        /// Установить зависимости
        /// </summary>
        public override void InstallBindings()
        {
            Container.Bind<IInputCatcher>().To<InputCatcher>().AsCached();
        }
    }
}