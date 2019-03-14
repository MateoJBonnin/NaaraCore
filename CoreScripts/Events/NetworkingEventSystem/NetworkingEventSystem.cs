using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkingEventSystem : AbstractEventSystem<INetworkEvent>
{
    public static NetworkingEventSystem instance;

    public override void Init()
    {
        instance = this;
    }

    internal void AddEventListener<T>()
    {
        throw new NotImplementedException();
    }
}