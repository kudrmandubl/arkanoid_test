using System.Collections.Generic;
using Screens.Implementations.Views;
using UnityEngine;

namespace Screens.Configs
{
    /// <summary>
    /// Конфиг экранов
    /// </summary>
    [CreateAssetMenu(fileName = "ScreensConfig", menuName = "Configs/Screens/ScreensConfig")]
    public class ScreensConfig : ScriptableObject
    {
        [SerializeField] private List<BaseScreen> _screenPrefabs;

        /// <summary>
        /// Префабы экранов
        /// </summary>
        public List<BaseScreen> ScreenPrefabs => _screenPrefabs;
    }
}