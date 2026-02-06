using UnityEngine;

namespace Common
{
    /// <summary>
    /// Канвас с ключом
    /// </summary>
    public class CanvasWithKey : MonoBehaviour
    {
        public const string LoadingSceneCanvasKey = "LoadingSceneCanvasKey";
        public const string GameSceneCanvasKey = "GameSceneCanvasKey";

        [SerializeField] private string _key;

        /// <summary>
        /// Ключ
        /// </summary>
        public string Key => _key;
    }
}
