using System;

namespace Pool
{
    public interface IPooleable
    {
        event Action<IPooleable> OnReturnedItem;

        void EnableObject();
        void DisableObject();
    }
}