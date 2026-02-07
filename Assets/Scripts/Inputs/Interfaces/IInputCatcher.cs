using System;
using UnityEngine;

namespace Inputs.Interfaces
{
    /// <summary>
    /// Ввод от пользователя
    /// </summary>
    public interface IInputCatcher
    {
        /// <summary>
        /// В кадр нажатия
        /// </summary>
        Action<Vector3> OnPointerDown { get; set; }

        /// <summary>
        /// Каждый кадр пока нажат
        /// </summary>
        Action<Vector3> OnPointer { get; set; }
        
        /// <summary>
        /// В кадр отжатия
        /// </summary>
        Action<Vector3> OnPointerUp { get; set; }
    }
}
