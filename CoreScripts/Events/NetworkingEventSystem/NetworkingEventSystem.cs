using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkingEventSystem : AbstractEventSystem<INetworkEvent>
{
    public static NetworkingEventSystem instance;

    public override void Init()
    {
        base.Init();
        instance = this;
    }
}