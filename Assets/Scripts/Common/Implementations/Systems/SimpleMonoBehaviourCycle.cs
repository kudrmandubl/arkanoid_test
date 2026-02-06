using System;
using Common.Interfaces;
using UnityEngine;

namespace Common.Implementations.Systems
{
    ///  <inheritdoc />
    public class SimpleMonoBehaviourCycle : MonoBehaviour, IMonoBehaviourCycle
    {
        ///  <inheritdoc />
        public Action<float> OnUpdate { get; set; }

        ///  <inheritdoc />
        public Action<float> OnFixedUpdate { get; set; }

        ///  <inheritdoc />
        public Action<bool> OnApplicationFocusChange { get; set; }

        /// <summary>
        /// Создать экземпляр
        /// </summary>
        /// <returns></returns>
        public static SimpleMonoBehaviourCycle CreateInstance()
        {
            var go = new GameObject($"{nameof(SimpleMonoBehaviourCycle)}");
            return go.AddComponent<SimpleMonoBehaviourCycle>();
        }

        /// <summary>
        /// Каждый кадр
        /// </summary>
        private void Update()
        {
            OnUpdate?.Invoke(Time.deltaTime);
        }

        /// <summary>
        /// Каждый расчёт физики
        /// </summary>
        private void FixedUpdate()
        {
            OnFixedUpdate?.Invoke(Time.fixedDeltaTime);
        }

        /// <summary>
        /// При смене фокуса приложения
        /// </summary>
        /// <param name="focus">Значение фокуса</param>
        private void OnApplicationFocus(bool focus)
        {
            OnApplicationFocusChange?.Invoke(focus);
        }
    }
}
