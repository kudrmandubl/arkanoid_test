using Common.Interfaces;
using UnityEngine;

namespace Common.Implementations.Systems
{
    ///  <inheritdoc />
    public class GameContainer : IGameContainer
    {
        private Transform _coreContainer;
        private Transform _metaContainer;
        private Transform _commonContainer;

        ///  <inheritdoc />
        public Transform CoreContainer => _coreContainer;

        ///  <inheritdoc />
        public Transform MetaContainer => _metaContainer;

        ///  <inheritdoc />
        public Transform CommonContainer => _commonContainer;

        /// <summary>
        /// Конструктор
        /// </summary>
        public GameContainer() 
        {
            CreateContainers();
        }

        /// <summary>
        /// Создать контейнеры
        /// </summary>
        private void CreateContainers()
        {
            _coreContainer = new GameObject("CoreContainer").transform;
            _metaContainer = new GameObject("MetaContainer").transform;
            _commonContainer = new GameObject("CommonContainer").transform;
        }
    }
}