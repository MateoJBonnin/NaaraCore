using Newtonsoft.Json;

public class LocalJSONStateDeserializer : AbstractLocalStateDeserializer
{
    private string jsonKey;

    public LocalJSONStateDeserializer(string jsonKey)
    {
        this.jsonKey = jsonKey;
    }

    public override StateSnapshot DeserializeState()
    {
        return JsonConvert.DeserializeObject<StateSnapshot>(this.jsonKey, new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All });
    }
}