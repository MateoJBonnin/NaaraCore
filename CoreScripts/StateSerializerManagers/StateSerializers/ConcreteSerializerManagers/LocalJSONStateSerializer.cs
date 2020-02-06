using Newtonsoft.Json;
using UnityEngine;

public class LocalJSONStateSerializer : AbstractLocalStateSerializer
{
    public string jsonGameSerializedString;

    public override void SerializeState(StateSnapshot stateSnapshot)
    {
        this.jsonGameSerializedString = JsonConvert.SerializeObject(stateSnapshot);
        Debug.LogError(jsonGameSerializedString);
    }
}