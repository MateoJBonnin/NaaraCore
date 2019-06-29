using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLNetworkingEventSystem : SubscriptionBasedEventSystem<LLINetworkEvent>
{
    public static LLNetworkingEventSystem instance;

    public override void Init()
    {
        base.Init();
        instance = this;
    }
}