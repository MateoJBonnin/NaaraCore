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

        public event Action<IPooleable> OnItemReturned;

        public void DisablePoolObject()
        {
            this.item.DisablePoolObject();
        }

        public void EnablePoolObject()
        {
            this.item.EnablePoolObject();
        }
    }
}
