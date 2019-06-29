using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEventSystem<T> where T : IEventeable
{
    public virtual void Init()
    {
        this.PrepareEventSystem();
    }

    protected virtual void PrepareEventSystem()
    {
    }

    public interface EventContainer
    {
    }
}