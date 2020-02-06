public abstract class AbstractExternalStateDeserializer<T> : AbstractStateDeserializer<T> where T : StateSnapshot
{
    protected string externalPath;

    protected AbstractExternalStateDeserializer(string externalPath)
    {
        this.externalPath = externalPath;
    }
}
