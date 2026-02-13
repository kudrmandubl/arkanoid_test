using System;
using Common.Interfaces;
using Common.Utils;

namespace Common.Implementations.Views
{
    ///  <inheritdoc cref="IParticlesView" />
    public class ParticlesView : CachedMonoBehaviour, IParticlesView
    {
        ///  <inheritdoc />
        public Action<IParticlesView> OnParticleStop { get; set; }

        /// <summary>
        /// Вызов в колбеке при окончании воспроизведения системы частиц
        /// </summary>
        public void OnParticleSystemStopped()
        {
            OnParticleStop?.Invoke(this);
        }
    }
}