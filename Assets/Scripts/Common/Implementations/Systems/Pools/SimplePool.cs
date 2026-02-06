
using Common.Interfaces;

namespace Common.Implementations.Systems.Pools
{
    ///  <inheritdoc cref="IPool{T}" />
    public class SimplePool<T1, T2> : BasePool<T1>, IPool<T1> 
        where T1 : class 
        where T2 : T1, new()
    {
        ///  <inheritdoc />
        protected override T1 GetNew()
        {
            return new T2();
        }

        ///  <inheritdoc />
        protected override void FreeExtraAction(T1 element)
        {
        }

        ///  <inheritdoc />
        protected override void GetFreeElementExtraAction(T1 element)
        {
        }
    }
}