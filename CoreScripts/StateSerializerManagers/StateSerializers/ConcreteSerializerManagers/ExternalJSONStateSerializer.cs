using System.IO;

public class ExternalJSONStateSerializer : AbstractExternalStateSerializer
{
    private LocalJSONStateSerializer jsonSerializer;

    public ExternalJSONStateSerializer(string externalPath) : base(externalPath)
    {
        this.jsonSerializer = new LocalJSONStateSerializer();
    }

    public override void SerializeState(StateSnapshot stateSnapshot)
    {
        this.jsonSerializer.SerializeState(stateSnapshot);
        File.WriteAllText(this.externalPath, this.jsonSerializer.jsonGameSerializedString);
    }
}
