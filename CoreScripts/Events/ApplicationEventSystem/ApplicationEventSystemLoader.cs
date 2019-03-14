using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationEventSystemLoader : AppManagaer
{
    private ApplicationEventSystem applicationEventSystem;

    public ApplicationEventSystemLoader()
    {
        this.applicationEventSystem = new ApplicationEventSystem();
        this.applicationEventSystem.Init();
    }
}