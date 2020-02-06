using System;
using System.Collections;
using System.Collections.Generic;

public class RequestReplyBasedEventSystem<T> : AbstractEventSystem<T> where T : IReplyEventeable
{
    protected GenericDatabase<Type, EventContainer> databaseStructure;

    public RequestReplyBasedEventSystem()
    {
    }

    public virtual void RequestData(IReplyEventeable gameEvent, Action<IReplyEventeable> OnEventReply)
    {
        //List<EventContainer> eventContainers = this.databaseStructure.GetData(gameEvent.GetType());
        //if (eventContainers == null) return;
        //ApplicationCoroutineManager acm = new ApplicationCoroutineManager();
        //acm.AppCoroutineStarter(eventListener.Raise(gameEvent, OnEventReply));
    }

    private IEnumerator EventWaiter(IEnumerator yielder)
    {
        yield return yielder;
    }

    public virtual void AddEventListener<W>(Func<W, W> eventAction) where W : T, IReplyEventeable
    {
        //SpecificEventContainer<W> eventContainer = new SpecificEventContainer<W>();
        //eventContainer.actionStorage = eventAction;
        //Type type = typeof(W);
        //this.databaseStructure.RegisterData(type, eventContainer);
        //eventLisnter.listenfor<type>(eventProcessorEvent)
    }

    // public virtual void AddEventListener<W>(Func<W> eventAction) where W : T, IReplyEventeable
    // {
    //     SpecificEventContainer eventContainer = new SpecificEventContainer();
    //     eventContainer.actionStorage = eventAction;
    //     Type type = typeof(W);
    //     this.databaseStructure.RegisterData(type, eventContainer);
    // }

    // public virtual void RemoveEventListener<W>(Action<W> eventAction) where W : T, IReplyEventeable
    // {
    //     Type type = typeof(W);
    //     this.databaseStructure.SearchAndRemoveDataByPredicate(type, (EventContainer container) => ((SpecificEventContainer<W>)container).actionStorage == eventAction);
    // }
    //
    // public virtual void RemoveEventListener<W>(Action eventAction) where W : T, IReplyEventeable
    // {
    //     Type type = typeof(W);
    //     this.databaseStructure.SearchAndRemoveDataByPredicate(type, (EventContainer container) => ((SpecificEventContainer)container).actionStorage == eventAction);
    // }
    //
    // public class ResponsiveEventContainer : DelayedEventContainer where R : IReplyEventeable
    // {
    //     public Func<IEventeable, IEnumerator<IEventeable>> actionStorage;
    //
    //     public IEnumerator Raise(IEventeable gameEvent, Action<IEventeable> responseAction)
    //     {
    //         yield return this.actionStorage(gameEvent);
    //         responseAction?.Invoke();
    //     }
    // }

    // public class ResponsiveEventContainer : DelayedEventContainer
    // {
    //     public Action actionStorage;
    //
    //     public void Raise(IEventeable gameEvent, Action<IEventeable> responseAction)
    //     {
    //         this.actionStorage();
    //     }
    // }

    public interface DelayedEventContainer : EventContainer
    {
        IEnumerable<Action<IEventeable>> Raise(Action<IEnumerable<IEventeable>> gameEvent, Action<IEventeable> responseAction);

    }

    protected override void PrepareEventSystem()
    {
        base.PrepareEventSystem();
        this.databaseStructure = new GenericDatabase<Type, EventContainer>();
        this.databaseStructure.ClearData();
    }
}