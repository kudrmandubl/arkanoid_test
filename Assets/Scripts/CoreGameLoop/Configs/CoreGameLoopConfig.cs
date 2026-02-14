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
        [SerializeField] private int _defaultTriesCount = 3;
        [SerializeField] private string _continueLabel = "Continue";
        [SerializeField] private string _restartLevelLabel = "Restart Level";
        [SerializeField] private string _startNewRunLabel = "Start New";
        [SerializeField] private LevelConfigData[] _levelConfigsData;

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

        /// <summary>
        /// Дефолтное количество попыток
        /// </summary>
        public int DefaultTriesCount => _defaultTriesCount;

        /// <summary>
        /// Надпись продолжить
        /// </summary>
        public string ContinueLabel => _continueLabel;

        /// <summary>
        /// Надпись перезапустить уровень
        /// </summary>
        public string RestartLevelLabel => _restartLevelLabel;

        /// <summary>
        /// Надпись начать новый забег
        /// </summary>
        public string StartNewRunLabel => _startNewRunLabel;

        /// <summary>
        /// Данные конфигов уровней
        /// </summary>
        public LevelConfigData[] LevelConfigsData => _levelConfigsData;
    }
}
