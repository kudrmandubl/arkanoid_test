using Buffs.Enums;
using UnityEngine;

namespace Buffs.Configs
{
    /// <summary>
    /// Конфиг бафа
    /// </summary>
    [CreateAssetMenu(fileName = "BuffConfig", menuName = "Configs/Buffs/BuffConfig")]
    public class BuffConfig : ScriptableObject
    {
        [SerializeField] private BuffType _buffType;
        [SerializeField] private int _intValue;
        [SerializeField] private float _floatValue;

        /// <summary>
        /// Тип бафа
        /// </summary>
        public BuffType BuffType => _buffType;

        /// <summary>
        /// Инт значение
        /// </summary>
        public float IntValue => _intValue;

        /// <summary>
        /// Флоат значение
        /// </summary>
        public float FloatValue => _floatValue;
    }
}
