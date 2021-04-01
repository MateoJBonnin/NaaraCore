using System.Collections.Generic;
using MEC;

public class CoroutineManager
{
    private const string DEFAULT_COROUTINE_TAG = "CoroutineNaaraCore";

    protected string CoroutinesTag
    {
        get { return DEFAULT_COROUTINE_TAG; }
    }

    protected string id;

    public virtual void Setup(string id)
    {
        this.id = id;
    }

    public CoroutineHandle AppCoroutineStarter(IEnumerator<float> coroutine)
    {
        return Timing.RunCoroutine(coroutine, this.CoroutinesTag);
    }

    public CoroutineHandle AppCoroutineStarter(IEnumerator<float> coroutine, Segment segment)
    {
        return Timing.RunCoroutine(coroutine, segment, this.CoroutinesTag);
    }

    public CoroutineHandle AppCoroutineStarter(IEnumerator<float> coroutine, string customTag, Segment segment)
    {
        return Timing.RunCoroutine(coroutine, segment, this.CoroutinesTag + customTag);
    }

    public CoroutineHandle AppCoroutineStarter(IEnumerator<float> coroutine, string customTag)
    {
        return Timing.RunCoroutine(coroutine, this.CoroutinesTag + customTag);
    }

    public void AppCoroutineStopper(CoroutineHandle coroutine)
    {
        Timing.KillCoroutines(coroutine);
    }

    public void AppCoroutineStopper(string customTag)
    {
        Timing.KillCoroutines(this.CoroutinesTag + customTag);
    }

    public void StopAllCoroutines()
    {
        Timing.KillCoroutines(this.CoroutinesTag);
    }
}
