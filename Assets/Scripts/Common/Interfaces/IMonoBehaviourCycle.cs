using System;

namespace Common.Interfaces
{
    /// <summary>
    /// Класса для взаимодействия с циклом жизни MonoBehaviour
    /// </summary>
    public interface IMonoBehaviourCycle
    {
        /// <summary>
        /// Каждый кадр
        /// </summary>
        Action<float> OnUpdate { get; set; }

        /// <summary>
        /// Каждый расчёт физики
        /// </summary>
        Action<float> OnFixedUpdate { get; set; }

        /// <summary>
        /// При смене фокуса приложения
        /// </summary>
        Action<bool> OnApplicationFocusChange { get; set; }
    }
}
