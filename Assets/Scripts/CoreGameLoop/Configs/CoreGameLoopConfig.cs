using CoreGameLoop.Implementations.Views;
using UnityEngine;

namespace CoreGameLoop.Configs
{
    /// <summary>
    /// Конфиг игрового цикла кора
    /// </summary>
    [CreateAssetMenu(fileName = "CoreGameLoopConfig", menuName = "Configs/CoreGameLoopConfig")]
    public class CoreGameLoopConfig : ScriptableObject
    {
        [SerializeField] private CoreBackView _coreBackViewPrefab;

        /// <summary>
        /// Префаб отображения фона кора
        /// </summary>
        public CoreBackView CoreBackViewPrefab => _coreBackViewPrefab;
    }
}
