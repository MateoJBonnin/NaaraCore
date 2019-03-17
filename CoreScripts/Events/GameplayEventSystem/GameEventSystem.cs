using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem : AbstractEventSystem<IGameEvent>
{
    public static GameEventSystem instance;

    public override void Init()
    {
        base.Init();
        instance = this;
    }
}