using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class BinaryExternalStateSerializer : AbstractExternalStateSerializer
{
    private BinaryFormatter binaryFormatter;
    private MemoryStream memoryStream;

    public BinaryExternalStateSerializer(string externalPath) : base(externalPath)
    {
        this.binaryFormatter = new BinaryFormatter();
        this.memoryStream = new MemoryStream();
    }

    public override void SerializeState(IStateSnapshot stateSnapshot)
    {
        this.binaryFormatter.Serialize(this.memoryStream, stateSnapshot);
        File.WriteAllBytes(this.externalPath, this.memoryStream.ToArray());
        this.memoryStream.SetLength(0);
    }
}
