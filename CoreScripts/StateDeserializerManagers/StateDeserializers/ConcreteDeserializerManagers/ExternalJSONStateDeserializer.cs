using System.IO;

public class ExternalJSONStateDeserializer<T> : AbstractExternalStateDeserializer<T> where T : IStateSnapshot
{
    private LocalJSONStateDeserializer<T> jsonDeserializer;

    public ExternalJSONStateDeserializer(string externalPath) : base(externalPath)
    {
        this.jsonDeserializer = new LocalJSONStateDeserializer<T>(File.ReadAllText(this.externalPath));
    }

    public override T DeserializeState()
    {
        return this.jsonDeserializer.DeserializeState();
    }
}
