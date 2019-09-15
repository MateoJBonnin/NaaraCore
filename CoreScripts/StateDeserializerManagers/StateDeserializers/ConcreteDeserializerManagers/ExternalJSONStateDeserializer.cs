using System.IO;

public class ExternalJSONStateDeserializer : AbstractExternalStateDeserializer
{
    private LocalJSONStateDeserializer jsonDeserializer;

    public ExternalJSONStateDeserializer(string externalPath) : base(externalPath)
    {
        this.jsonDeserializer = new LocalJSONStateDeserializer(File.ReadAllText(this.externalPath));
    }

    public override StateSnapshot DeserializeState()
    {
        return this.jsonDeserializer.DeserializeState();
    }
}
