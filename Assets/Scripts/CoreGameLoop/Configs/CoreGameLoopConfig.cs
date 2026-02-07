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
        [SerializeField] private string _winLabel = "Win";
        [SerializeField] private string _loseLabel = "Lose";

        /// <summary>
        /// Префаб отображения фона кора
        /// </summary>
        public CoreBackView CoreBackViewPrefab => _coreBackViewPrefab;

        /// <summary>
        /// Надпись победы
        /// </summary>
        public string WinLabel => _winLabel;

        /// <summary>
        /// Надпись проигрыша
        /// </summary>
        public string LoseLabel => _loseLabel;
    }
}
