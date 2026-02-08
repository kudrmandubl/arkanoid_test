using System.Collections.Generic;
using Balls.Interfaces;
using Buffs.Configs;
using Buffs.Data;
using Buffs.Interfaces;
using Common.Interfaces;
using GameField.Interfaces;
using UnityEngine;

namespace Buffs.Implementations.Systems
{
    ///  <inheritdoc />
    public class BuffSystem : IBuffSystem
    {
        private IMonoBehaviourPool<IBuffView> _buffViewPool;
        private IPool<BuffData> _buffDataPool;
        private IGameContainer _gameContainer;
        private IBallCollisionProcessor _ballCollisionProcessor;
        private IMonoBehaviourCycle _monoBehaviourCycle;
        private IMainCamera _mainCamera;

        private BuffsConfig _buffsConfig;

        private List<IBuffView> _buffViews;
        private bool _isActive;

        /// <summary>
        /// Конструктор
        /// </summary>
        public BuffSystem(IMonoBehaviourPool<IBuffView> buffViewPool,
            IPool<BuffData> buffDataPool,
            IGameContainer gameContainer,
            IBallCollisionProcessor ballCollisionProcessor,
            IMonoBehaviourCycle monoBehaviourCycle,
            IMainCamera mainCamera,
            BuffsConfig buffsConfig)
        {
            _buffViewPool = buffViewPool;
            _buffDataPool = buffDataPool;
            _gameContainer = gameContainer;
            _ballCollisionProcessor = ballCollisionProcessor;
            _monoBehaviourCycle = monoBehaviourCycle;
            _mainCamera = mainCamera;

            _buffsConfig = buffsConfig;

            _monoBehaviourCycle.OnFixedUpdate += MoveBuffs;

            _buffViewPool.SetPrefab(_buffsConfig.BuffViewPrefab);
            _buffViewPool.SetContainer(_gameContainer.CoreContainer);

            _buffViews = new List<IBuffView>();
        }

        ///  <inheritdoc />
        public void Initialize()
        {
            _ballCollisionProcessor.OnDestroyGameFieldCelLView += CreateBuff;
        }

        ///  <inheritdoc />
        public void Clear()
        {
            for (int i = _buffViews.Count - 1; i >= 0; i--)
            {
                var buffView = _buffViews[i];
                DestroyBuff(buffView);
            }
        }

        ///  <inheritdoc />
        public void SetActive(bool value)
        {
            _isActive = value;
        }

        /// <summary>
        /// Создать бафф
        /// </summary>
        private void CreateBuff(IGameFieldCellView gameFieldCellView)
        {
            if (UnityEngine.Random.value > _buffsConfig.BuffChance)
            {
                return;
            }

            var randomBuffConfig = _buffsConfig.BuffConfigs[UnityEngine.Random.Range(0, _buffsConfig.BuffConfigs.Length)];
            var buffView = _buffViewPool.GetFreeElement();
            buffView.Transform.position = gameFieldCellView.Transform.position;

            buffView.BuffData = _buffDataPool.GetFreeElement();

            buffView.BuffData.BuffType = randomBuffConfig.BuffType;
            buffView.BuffData.IntValue = randomBuffConfig.IntValue;
            buffView.BuffData.FloatValue = randomBuffConfig.FloatValue;

            _buffViews.Add(buffView);
        }

        /// <summary>
        /// Уничтожить баф
        /// </summary>
        /// <param name="buffView"></param>
        private void DestroyBuff(IBuffView buffView)
        {
            _buffDataPool.Free(buffView.BuffData);
            _buffViewPool.Free(buffView);
            _buffViews.Remove(buffView);
        }

        /// <summary>
        /// Переместить бафы
        /// </summary>
        private void MoveBuffs(float deltaTime)
        {
            if (!_isActive)
            {
                return;
            }

            for (int i = _buffViews.Count - 1; i >= 0; i--)
            {
                var buffView = _buffViews[i];
                var position = buffView.Transform.position + Vector3.down * _buffsConfig.BuffSpeed * deltaTime;
                var isBuffLost = CheckBorders(position);
                buffView.Rigidbody2D.MovePosition(position);

                if (isBuffLost)
                {
                    DestroyBuff(buffView);
                }
            }
        }

        /// <summary>
        /// Применить границы
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private bool CheckBorders(Vector3 position)
        {
            //Получить границы экрана в мировых координатах
            var bottomLeftPosition = _mainCamera.Camera.ScreenToWorldPoint(new Vector3(0, 0, _mainCamera.Camera.transform.position.z));
            float screenBottom = bottomLeftPosition.y;

            if (position.y <= screenBottom)
            {
                return true;
            }

            return false;
        }
    }
}
