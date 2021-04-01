using Newtonsoft.Json;
using UnityEngine;

public class LocalJSONStateSerializer : AbstractLocalStateSerializer
{
    public string jsonGameSerializedString;

    public override void SerializeState(IStateSnapshot stateSnapshot)
    {
        this.jsonGameSerializedString = JsonConvert.SerializeObject(stateSnapshot);
        Debug.LogError(jsonGameSerializedString);
    }
}