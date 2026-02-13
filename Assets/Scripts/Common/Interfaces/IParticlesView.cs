using System;

namespace Common.Interfaces
{
    /// <summary>
    /// Отображение частич
    /// </summary>
    public interface IParticlesView : IBaseView
    {
        /// <summary>
        /// При остановке части
        /// </summary>
        Action<IParticlesView> OnParticleStop { get; set; }
    }
}