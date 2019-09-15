public abstract class AbstractExternalStateSerializer : AbstractStateSerializer
{
    protected string externalPath;

    protected AbstractExternalStateSerializer(string externalPath)
    {
        this.externalPath = externalPath;
    }
}
