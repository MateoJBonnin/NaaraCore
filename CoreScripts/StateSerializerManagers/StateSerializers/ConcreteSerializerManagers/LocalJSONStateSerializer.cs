using Newtonsoft.Json;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class LocalJSONStateSerializer : AbstractLocalStateSerializer
{
    public string jsonGameSerializedString;

    public override void SerializeState(StateSnapshot stateSnapshot)
    {
        this.jsonGameSerializedString = JsonConvert.SerializeObject(stateSnapshot, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
        Debug.LogError(jsonGameSerializedString);
    }
}