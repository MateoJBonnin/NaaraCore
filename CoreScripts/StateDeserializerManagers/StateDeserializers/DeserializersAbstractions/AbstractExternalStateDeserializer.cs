public abstract class AbstractExternalStateDeserializer<T> : AbstractStateDeserializer<T> where T : IStateSnapshot
{
    protected string externalPath;

    protected AbstractExternalStateDeserializer(string externalPath)
    {
        this.externalPath = externalPath;
    }
}
