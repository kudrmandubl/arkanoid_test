using UnityEngine;

namespace Common.Utils
{
    /// <summary>
    /// Кэшированный моно бех
    /// </summary>
    public class CachedMonoBehaviour : MonoBehaviour
    {
        private Transform _transform;

        /// <summary>
        /// Трансформ
        /// </summary>
        public Transform Transform
        {
            get
            {
                if (!_transform)
                {
                    _transform = transform;
                }

                return _transform;
            }
        }
    }
}