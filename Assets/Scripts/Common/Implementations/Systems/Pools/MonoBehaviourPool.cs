using Common.Interfaces;
using UnityEngine;

namespace Common.Implementations.Systems.Pools
{
    ///  <inheritdoc cref="IMonoBehaviourPool{T1}" />
    public class MonoBehaviourPool<T1, T2> : BasePool<T1>, IMonoBehaviourPool<T1> where T2 : MonoBehaviour, T1
    {
        private T2 _prefab;
        private Transform _container;

        ///  <inheritdoc />
        public void SetPrefab(T1 prefab)
        {
            _prefab = prefab as T2;
        }

        ///  <inheritdoc />
        public void SetContainer(Transform container)
        {
            _container = container;
        }

        ///  <inheritdoc />
        protected override T1 GetNew()
        {
            return GameObject.Instantiate(_prefab, _container);
        }

        ///  <inheritdoc />
        protected override void FreeExtraAction(T1 element)
        {
            (element as T2).gameObject.SetActive(false);
        }

        ///  <inheritdoc />
        protected override void GetFreeElementExtraAction(T1 element)
        {
            (element as T2).gameObject.SetActive(true);
        }
    }
}