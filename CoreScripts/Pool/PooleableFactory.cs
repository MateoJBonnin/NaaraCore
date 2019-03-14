using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Factory;
using Managers;

namespace Pool
{
    public class PooleableFactory<T> : FactoryCreator<T> where T : IPooleable
    {
        public Action OnInitPoolFinished;

        private const int POOL_DEFAULT_STARTAMOUNT = 10;
        private const int TIME_SLICING_COUNT = 250;

        private int startAmount;

        private Queue<PooleableObject<T>> itemQueue;
        private Queue<PooleableObject<T>> unusedQueue;

        public int StartAmount
        {
            get { return startAmount; }
            set { startAmount = Mathf.Max(value, 0); }
        }

        public PooleableFactory(Func<T> getItem, int startAmount = 0, Action OnInitPoolFinished = null) : base(getItem)
        {
            this.unusedQueue = new Queue<PooleableObject<T>>();
            this.itemQueue = new Queue<PooleableObject<T>>(StartAmount);
            this.StartAmount = startAmount;
            this.OnInitPoolFinished += OnInitPoolFinished;
            this.InitialPool();
        }

        public PooleableFactory(Func<T> itemCreation, Action OnInitPoolFinished = null) : this(itemCreation, PooleableFactory<PooleableObject<T>>.POOL_DEFAULT_STARTAMOUNT, OnInitPoolFinished)
        {
        }

        public T GetPoolItem()
        {
            T item = default(T);
            if (0 < this.itemQueue.Count)
            {
                PooleableObject<T> poolObject = this.itemQueue.Dequeue();
                this.unusedQueue.Enqueue(poolObject);
                item = poolObject.item;
            }
            else
                item = this.Create();

            return item;
        }

        public void ReturnPoolItem(T item)
        {
            PooleableObject<T> poolObject = null;

            if (0 < this.unusedQueue.Count)
            {
                poolObject = this.unusedQueue.Dequeue();
                poolObject.item = item;
            }
            else
                poolObject = new PooleableObject<T>(item);

            this.itemQueue.Enqueue(poolObject);
        }

        private void InitialPool()
        {
            GameCoroutineManager.instance.StartCoroutine(this.LazyCreate(PooleableFactory<T>.TIME_SLICING_COUNT, this.StartAmount, (item) =>
             {
                 ((IPooleable)item).DisableObject();
                 this.itemQueue.Enqueue(new PooleableObject<T>(item));
                 return item;
             }, () =>
             {
                 this.OnInitPoolFinished?.Invoke();
             }
             ));
        }
    }
}
