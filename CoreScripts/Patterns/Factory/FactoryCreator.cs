using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Factory
{
    public class FactoryCreator<T>
    {
        protected Action<T> OnItemCreated = delegate { };
        private Func<T> getItem;

        public FactoryCreator(Func<T> getItem)
        {
            this.getItem = getItem;
        }

        public IEnumerator LazyCreate(int timeSlicingCount, int creationCount, Func<T, T> processItem, Action onComplete)
        {
            int count = 0;
            for (int i = 0; i <= creationCount; i++)
            {
                processItem(this.Create());
                if (count > timeSlicingCount)
                {
                    count = 0;
                    yield return new WaitForEndOfFrame();
                }
                else
                    count++;
            }
            onComplete();
        }

        protected virtual T Create()
        {
            T item = getItem();
            this.OnItemCreated?.Invoke(item);
            return item;
        }
    }
}