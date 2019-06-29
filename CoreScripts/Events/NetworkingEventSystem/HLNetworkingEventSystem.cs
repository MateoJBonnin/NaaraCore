using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HLNetworkingEventSystem : SubscriptionBasedEventSystem<HLINetworkEvent>
{
    public static HLNetworkingEventSystem instance;

    public override void Init()
    {
        base.Init();
        instance = this;
    }
}