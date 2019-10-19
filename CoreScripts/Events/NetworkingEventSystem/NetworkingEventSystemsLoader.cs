using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkingEventSystemsLoader : AppManager
{
    private LLNetworkingEventSystem lLNetworkingEventSystem;
    private HLNetworkingEventSystem hLNetworkingEventSystem;

    public NetworkingEventSystemsLoader()
    {
        this.lLNetworkingEventSystem = new LLNetworkingEventSystem();
        this.hLNetworkingEventSystem = new HLNetworkingEventSystem();
        this.lLNetworkingEventSystem.Init();
        this.hLNetworkingEventSystem.Init();
    }
}