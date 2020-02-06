using System;
using System.Collections.Generic;

public class SubscriptionBasedEventSystem<T> : AbstractEventSystem<T> where T : ISubscriptionEventeable
{
    protected GenericDatabase<Type, InstantEventContainer> databaseStructure;

    public virtual void DispatchEvent(ISubscriptionEventeable gameEvent)
    {
        List<InstantEventContainer> eventContainers = this.databaseStructure.GetData(gameEvent.GetType());
        if (eventContainers == null) return;

        for (int i = eventContainers.Count - 1; i >= 0; i--)
        {
            eventContainers[i].Raise(gameEvent);
        }
    }

    public virtual void AddEventListener<W>(Action<W> eventAction) where W : T, ISubscriptionEventeable
    {
        SpecificEventContainer<W> eventContainer = new SpecificEventContainer<W>();
        eventContainer.actionStorage = eventAction;
        Type type = typeof(W);
        this.databaseStructure.RegisterData(type, eventContainer);
    }

    public virtual void AddEventListener<W>(Action eventAction) where W : T, ISubscriptionEventeable
    {
        SpecificEventContainer eventContainer = new SpecificEventContainer();
        eventContainer.actionStorage = eventAction;
        Type type = typeof(W);
        this.databaseStructure.RegisterData(type, eventContainer);
    }

    public virtual void RemoveEventListener<W>(Action<W> eventAction) where W : T, ISubscriptionEventeable
    {
        Type type = typeof(W);
        this.databaseStructure.SearchAndRemoveDataByPredicate(type, (InstantEventContainer container) => ((SpecificEventContainer<W>)container).actionStorage == eventAction);
    }

    public virtual void RemoveEventListener<W>(Action eventAction) where W : T, ISubscriptionEventeable
    {
        Type type = typeof(W);
        this.databaseStructure.SearchAndRemoveDataByPredicate(type, (InstantEventContainer container) => ((SpecificEventContainer)container).actionStorage == eventAction);
    }

    public class SpecificEventContainer<R> : InstantEventContainer where R : ISubscriptionEventeable
    {
        public Action<R> actionStorage;

        public void Raise(IEventeable gameEvent)
        {
            this.actionStorage((R)gameEvent);
        }
    }

    public class SpecificEventContainer : InstantEventContainer
    {
        public Action actionStorage;

        public void Raise(IEventeable gameEvent)
        {
            this.actionStorage();
        }
    }

    public interface InstantEventContainer : EventContainer
    {
        void Raise(IEventeable gameEvent);
    }

    protected override void PrepareEventSystem()
    {
        base.PrepareEventSystem();
        this.databaseStructure = new GenericDatabase<Type, InstantEventContainer>();
        this.databaseStructure.ClearData();
    }
}