using System.Linq;
using Common.Interfaces;
using UnityEngine;

namespace Common.Implementations.Systems
{
    ///  <inheritdoc />
    public class MainCamera : IMainCamera
    {
        private UnityEngine.Camera _camera;

        public UnityEngine.Camera Camera => _camera;

        /// <summary>
        /// Конструктор
        /// </summary>
        public MainCamera()
        {
        }

        ///  <inheritdoc />
        public void Initialize(string cameraKey)
        {
            var camera = GameObject.FindObjectsByType<CameraWithKey>(FindObjectsSortMode.None)
                .FirstOrDefault(x => x.Key == cameraKey)
                .GetComponent<Camera>();
            _camera = camera;
        }

        ///  <inheritdoc />
        public void SetPerspective()
        {
            _camera.orthographic = false;
        }

        ///  <inheritdoc />
        public void SetOrthographic()
        {
            _camera.orthographic = true;
        }
    }
}
