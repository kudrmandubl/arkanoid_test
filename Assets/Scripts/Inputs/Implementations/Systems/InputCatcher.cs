using System;
using Common.Interfaces;
using Inputs.Interfaces;
using UnityEngine;

namespace Inputs.Implementations.Systems
{
    ///  <inheritdoc />
    public class InputCatcher : IInputCatcher
    {
        private IMonoBehaviourCycle _monoBehaviourCycle;

        ///  <inheritdoc />
        public Action<Vector3> OnPointerDown { get; set; }

        ///  <inheritdoc />
        public Action<Vector3> OnPointer { get; set; }

        ///  <inheritdoc />
        public Action<Vector3> OnPointerUp { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public InputCatcher(IMonoBehaviourCycle monoBehaviourCycle) 
        {
            _monoBehaviourCycle = monoBehaviourCycle;

            _monoBehaviourCycle.OnUpdate += Update;
        }

        /// <summary>
        /// Каждый кадр
        /// </summary>
        private void Update(float deltaTime)
        {
            var pointerPosition = Input.mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                OnPointerDown?.Invoke(pointerPosition);
            }

            if (Input.GetMouseButton(0))
            {
                OnPointer?.Invoke(pointerPosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnPointerUp?.Invoke(pointerPosition);
            }
        }
    }
}