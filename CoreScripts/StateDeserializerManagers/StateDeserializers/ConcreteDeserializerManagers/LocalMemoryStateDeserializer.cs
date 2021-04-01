public class LocalMemoryStateDeserializer<T> : AbstractLocalStateDeserializer<T> where T : IStateSnapshot
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