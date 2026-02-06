using UnityEngine;

namespace Common
{
    /// <summary>
    /// Камера с ключом
    /// </summary>
    public class CameraWithKey : MonoBehaviour
    {
        public const string LoadingSceneCameraKey = "LoadingSceneCameraKey";
        public const string GameSceneCameraKey = "GameSceneCameraKey";

        [SerializeField] private string _key;

        /// <summary>
        /// Ключ
        /// </summary>
        public string Key => _key;
    }
}
