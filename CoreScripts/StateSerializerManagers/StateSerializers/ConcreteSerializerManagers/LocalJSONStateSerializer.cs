using Newtonsoft.Json;

public class LocalJSONStateSerializer : AbstractLocalStateSerializer
{
    public string jsonGameSerializedString;

    public override void SerializeState(IStateSnapshot stateSnapshot)
    {
        this.jsonGameSerializedString = JsonConvert.SerializeObject(stateSnapshot);
    }
}
