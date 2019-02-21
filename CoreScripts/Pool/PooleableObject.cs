using System;

namespace Pool
{
    public class PooleableObject<T> : IPooleable where T : IPooleable
    {
        public T item;

        public PooleableObject(T item)
        {
            this.item = item;
        }

        public event Action<IPooleable> OnReturnedItem;

        public void DisableObject()
        {
            item.DisableObject();
        }

        public void EnableObject()
        {
            item.EnableObject();
        }
    }
}