using System;
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

        ///  <inheritdoc />
        public Action OnCameraSizeChange { get; set; }

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
            var newSize = 0f;
            if (currentAspectRatio < targetAspectRatio)
            {
                // Рассчитываем нужный orthographicSize так, чтобы ширина оставалась постоянной
                newSize = _initialOrthographicSize * (targetAspectRatio / currentAspectRatio);
            }
            else
            {
                newSize = _initialOrthographicSize;
            }

            if(!Mathf.Approximately(_mainCamera.Camera.orthographicSize, newSize))
            {
                _mainCamera.Camera.orthographicSize = newSize;
                OnCameraSizeChange?.Invoke();
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