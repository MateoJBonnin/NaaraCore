using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class ExternalBinaryStateDeserializer<T> : AbstractExternalStateDeserializer<T> where T : StateSnapshot
{
    private BinaryFormatter binaryFormatter;
    private MemoryStream memoryStream;

    public ExternalBinaryStateDeserializer(string externalPath) : base(externalPath)
    {
        this.binaryFormatter = new BinaryFormatter();
        this.memoryStream = new MemoryStream();
    }

    public override T DeserializeState()
    {
        byte[] stateInBytes = File.ReadAllBytes(this.externalPath);
        this.memoryStream.Write(stateInBytes, 0, stateInBytes.Length);
        T stateSnapshot = (T)this.binaryFormatter.Deserialize(this.memoryStream);
        this.memoryStream.SetLength(0);
        return stateSnapshot;
    }
}