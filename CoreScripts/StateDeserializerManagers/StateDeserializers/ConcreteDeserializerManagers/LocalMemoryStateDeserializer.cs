public class LocalMemoryStateDeserializer<T> : AbstractLocalStateDeserializer<T> where T : StateSnapshot
{
    private T cachedState;

    public LocalMemoryStateDeserializer(T cachedState)
    {
        this.cachedState = cachedState;
    }

    public override T DeserializeState()
    {
        return this.cachedState;
    }
}