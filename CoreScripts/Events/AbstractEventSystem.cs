using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEventSystem<T> where T : IEventeable
{
    protected DictionaryWithListDatabaseStructure<Type, EventContainer> databaseStructure;

    public virtual void Init()
    {
        this.databaseStructure = new DictionaryWithListDatabaseStructure<Type, EventContainer>();
    }

    public void DispatchEvent(IEventeable gameEvent)
    {
        List<EventContainer> eventContainers = this.databaseStructure.GetData(gameEvent.GetType());
        if (eventContainers == null) return;

        for (int i = eventContainers.Count - 1; i >= 0; i--)
        {
            eventContainers[i].Raise(gameEvent);
        }
    }

    public void AddEventListener<W>(Action<W> eventAction) where W : T, IEventeable
    {
        SpecificEventContainer<W> eventContainer = new SpecificEventContainer<W>();
        eventContainer.actionStorage = eventAction;
        Type type = typeof(W);
        this.databaseStructure.RegisterData(type, eventContainer);
    }

    public void AddEventListener<W>(Action eventAction) where W : T, IEventeable
    {
        SpecificEventContainer eventContainer = new SpecificEventContainer();
        eventContainer.actionStorage = eventAction;
        Type type = typeof(W);
        this.databaseStructure.RegisterData(type, eventContainer);
    }

    public void RemoveEventListener<W>(Action<W> eventAction) where W : T, IEventeable
    {
        Type type = typeof(W);
        this.databaseStructure.SearchAndRemoveDataByPredicate(type, (EventContainer container) => ((SpecificEventContainer<W>)container).actionStorage == eventAction);
    }

    public void RemoveEventListener<W>(Action eventAction) where W : T, IEventeable
    {
        Type type = typeof(W);
        this.databaseStructure.SearchAndRemoveDataByPredicate(type, (EventContainer container) => ((SpecificEventContainer)container).actionStorage == eventAction);
    }

    protected void PrepareEventSystem()
    {
        this.databaseStructure = new DictionaryWithListDatabaseStructure<Type, EventContainer>();
        this.databaseStructure.ClearData();
    }

    public class SpecificEventContainer<R> : EventContainer where R : IEventeable
    {
        public Action<R> actionStorage;

        public void Raise(IEventeable gameEvent)
        {
            this.actionStorage((R)gameEvent);
        }
    }

    public class SpecificEventContainer : EventContainer
    {
        public Action actionStorage;

        public void Raise(IEventeable gameEvent)
        {
            this.actionStorage();
        }
    }

    public interface EventContainer
    {
        void Raise(IEventeable gameEvent);
    }
}