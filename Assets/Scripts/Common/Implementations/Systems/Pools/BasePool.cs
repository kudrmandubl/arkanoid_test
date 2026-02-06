using System.Collections.Generic;

namespace Common.Implementations.Systems.Pools
{
    /// <summary>
    /// Базовый пул
    /// </summary>
    public abstract class BasePool<T>
    {
        private Dictionary<T, bool> _elementToFreePairs;

        /// <summary>
        /// Конструктор
        /// </summary>
        protected BasePool()
        {
            _elementToFreePairs = new Dictionary<T, bool>();
        }

        /// <summary>
        /// Получить свободный элемент
        /// </summary>
        /// <returns>Свободный элемент</returns>
        public T GetFreeElement()
        {
            foreach (var pair in _elementToFreePairs)
            {
                if (pair.Value)
                {
                    _elementToFreePairs[pair.Key] = false;
                    GetFreeElementExtraAction(pair.Key);
                    return pair.Key;
                }
            }

            var newElement = GetNew();
            _elementToFreePairs.Add(newElement, false);

            GetFreeElementExtraAction(newElement);
            return newElement;
        }

        /// <summary>
        /// Освободить
        /// </summary>
        /// <param name="element">Элемент для освободения</param>
        public void Free(T element)
        {
            _elementToFreePairs[element] = true;
            FreeExtraAction(element);
        }

        /// <summary>
        /// Получить новый элемент
        /// </summary>
        /// <returns>Новый элемент</returns>
        protected abstract T GetNew();

        /// <summary>
        /// Дополнительное действие при получении свободного элемента
        /// </summary>
        protected abstract void GetFreeElementExtraAction(T element);

        /// <summary>
        /// Дополнительное действие при освобождении элемента
        /// </summary>
        protected abstract void FreeExtraAction(T element);
    }
}