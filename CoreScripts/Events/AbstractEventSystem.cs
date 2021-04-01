using UnityEngine;

public abstract class AbstractEventSystem<T> : MonoBehaviour where T : IEventeable
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
