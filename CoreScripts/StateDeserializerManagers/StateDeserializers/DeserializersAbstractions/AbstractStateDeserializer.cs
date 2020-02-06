public abstract class AbstractStateDeserializer<T> where T : StateSnapshot
{
    public abstract T DeserializeState();
}