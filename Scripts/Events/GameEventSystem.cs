using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem instance;

    public DictionaryWithListDatabaseStructure<Type, EventContainer> databaseStructure;

    public void Init()
    {
        instance = this;
        this.databaseStructure = new DictionaryWithListDatabaseStructure<Type, EventContainer>();
        this.databaseStructure.ClearData();
    }

    public void DispatchEvent(IGameEvent gameEvent)
    {
        List<EventContainer> eventContainers = this.databaseStructure.GetData(gameEvent.GetType());
        if (eventContainers == null) return;

        for (int i = eventContainers.Count - 1; i >= 0; i--)
            eventContainers[i].Raise(gameEvent);
    }

    public void AddEventListener<T>(Action<T> eventAction) where T : IGameEvent
    {
        SpecificEventContainer<T> eventContainer = new SpecificEventContainer<T>();
        eventContainer.actionStorage = eventAction;
        Type type = typeof(T);
        this.databaseStructure.RegisterData(type, eventContainer);
    }

    public void AddEventListener<T>(Action eventAction) where T : IGameEvent
    {
        SpecificEventContainer eventContainer = new SpecificEventContainer();
        eventContainer.actionStorage = eventAction;
        Type type = typeof(T);
        this.databaseStructure.RegisterData(type, eventContainer);
    }

    public void RemoveEventListener<T>(Action<T> eventAction) where T : IGameEvent
    {
        Type type = typeof(T);
        this.databaseStructure.SearchAndRemoveDataByPredicate(type, (EventContainer container) => ((SpecificEventContainer<T>)container).actionStorage == eventAction);
    }

    public void RemoveEventListener<T>(Action eventAction) where T : IGameEvent
    {
        Type type = typeof(T);
        this.databaseStructure.SearchAndRemoveDataByPredicate(type, (EventContainer container) => ((SpecificEventContainer)container).actionStorage == eventAction);
    }

    public class SpecificEventContainer<T> : EventContainer where T : IGameEvent
    {
        public Action<T> actionStorage;

        public void Raise(IGameEvent gameEvent)
        {
            this.actionStorage((T)gameEvent);
        }
    }

    public class SpecificEventContainer : EventContainer
    {
        public Action actionStorage;

        public void Raise(IGameEvent gameEvent)
        {
            this.actionStorage();
        }
    }

    public interface EventContainer
    {
        void Raise(IGameEvent gameEvent);
    }
}