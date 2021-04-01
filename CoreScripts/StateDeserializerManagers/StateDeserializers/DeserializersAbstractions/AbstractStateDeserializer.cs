public abstract class AbstractStateDeserializer<T> where T : IStateSnapshot
{
    public abstract T DeserializeState();
}