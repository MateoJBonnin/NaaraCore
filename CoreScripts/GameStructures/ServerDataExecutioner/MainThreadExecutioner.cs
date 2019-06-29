using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class MainThreadExecutioner
{
    public Queue<Action> dataActionQueue;

    public MainThreadExecutioner()
    {
        this.dataActionQueue = new Queue<Action>();
    }

    public void Execute(Action actionToWait)
    {
        this.dataActionQueue.Enqueue(actionToWait);
    }

    public void UpdateDataProcessor()
    {
        if (this.dataActionQueue.IsEmpty())
            return;

        Action triggeredDelegate = this.dataActionQueue.Dequeue();
        triggeredDelegate.Invoke();
    }
}



//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Net;
//using UnityEngine;

//public class MainThreadExecutioner
//{
//    public GenericDatabaseStructure<Type, Action<LLINetworkEvent>> actionDataProcessor;
//    public Queue<LLINetworkEvent> dataActionQueue;

//    private delegate void CallbackerDelegate(Delegate actionToWait);
//    private CallbackerDelegate callbackerDelegate;

//    public MainThreadExecutioner()
//    {
//        this.actionDataProcessor = new GenericDatabaseStructure<Type, Action<LLINetworkEvent>>();
//        this.dataActionQueue = new Queue<LLINetworkEvent>();
//    }

//    public void SubscribeAction<T>(Action<LLINetworkEvent> actionToTrigger) where T : LLINetworkEvent
//    {
//        this.actionDataProcessor.RegisterData(typeof(T), actionToTrigger);
//    }

//    public void UnsubscribeAction<T>(Action<LLINetworkEvent> actionToTrigger) where T : LLINetworkEvent
//    {
//        this.actionDataProcessor.RemoveData(typeof(T), actionToTrigger);
//    }

//    public void ExecuteWith(LLINetworkEvent actionToWait)
//    {
//        this.dataActionQueue.Enqueue(actionToWait);
//    }

//    public void UpdateDataProcessor()
//    {
//        if (this.dataActionQueue.IsEmpty())
//            return;

//        LLINetworkEvent triggeredDelegate = this.dataActionQueue.Dequeue();

//        List<Action<LLINetworkEvent>> actionsToTrigger = actionDataProcessor.GetData(triggeredDelegate.GetType());

//        for (int i = actionsToTrigger.Count - 1; i >= 0; i--)
//            actionsToTrigger[i].Invoke(triggeredDelegate);
//    }
//}