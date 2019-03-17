using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationEventSystem : AbstractEventSystem<IAppEvent>
{
    public static ApplicationEventSystem instance;

    public override void Init()
    {
        base.Init();
        instance = this;
    }
}