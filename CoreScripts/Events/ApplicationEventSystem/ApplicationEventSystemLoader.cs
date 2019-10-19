using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationEventSystemLoader : AppManager
{
    private ApplicationEventSystem applicationEventSystem;

    public ApplicationEventSystemLoader()
    {
        this.applicationEventSystem = new ApplicationEventSystem();
        this.applicationEventSystem.Init();
    }
}