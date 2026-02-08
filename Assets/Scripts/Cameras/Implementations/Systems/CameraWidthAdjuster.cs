using Cameras.Configs;
using Cameras.Interfaces;
using Common.Interfaces;
using UnityEngine;

namespace Cameras.Implementations.Systems
{
    ///  <inheritdoc />
    public class CameraWidthAdjuster : ICameraWidthAdjuster
    {
        private IMonoBehaviourCycle _monoBehaviourCycle;
        private IMainCamera _mainCamera;

        private CameraConfig _cameraConfig; 

        private float _initialOrthographicSize;

        /// <summary>
        /// Конструктор
        /// </summary>
        public CameraWidthAdjuster(IMonoBehaviourCycle monoBehaviourCycle,
            IMainCamera mainCamera,
            CameraConfig cameraConfig)
        {
            _monoBehaviourCycle = monoBehaviourCycle;
            _mainCamera = mainCamera;

            _cameraConfig = cameraConfig;

            if (_cameraConfig.UpdateEveryFrame)
            {
                _monoBehaviourCycle.OnUpdate += Update;
            }
        }

        ///  <inheritdoc />
        public void Initialize()
        {
            _initialOrthographicSize = _mainCamera.Camera.orthographicSize;

            UpdateCameraSize();
        }

        ///  <inheritdoc />
        public void UpdateCameraSize()
        {
            var targetAspectRatio = _cameraConfig.AspectRatioWidth / _cameraConfig.AspectRatioHeight;
            var currentAspectRatio = _mainCamera.Camera.aspect;
            if (currentAspectRatio < targetAspectRatio)
            {
                // Рассчитываем нужный orthographicSize так, чтобы ширина оставалась постоянной
                _mainCamera.Camera.orthographicSize = _initialOrthographicSize * (targetAspectRatio / currentAspectRatio);
            }
            else
            {
                _mainCamera.Camera.orthographicSize = _initialOrthographicSize;
            }
        }

        /// <summary>
        /// Каждый кадр
        /// </summary>
        private void Update(float deltaTime)
        {
            UpdateCameraSize();
        }
    }
}