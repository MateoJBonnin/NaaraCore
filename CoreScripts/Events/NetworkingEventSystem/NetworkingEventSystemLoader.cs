using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkingEventSystemLoader : AppManagaer
{
    private NetworkingEventSystem networkingEventSystem;

    public NetworkingEventSystemLoader()
    {
        this.networkingEventSystem = new NetworkingEventSystem();
        this.networkingEventSystem.Init();
    }
}