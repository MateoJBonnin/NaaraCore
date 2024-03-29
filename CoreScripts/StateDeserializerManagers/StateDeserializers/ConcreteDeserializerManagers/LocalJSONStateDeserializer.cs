﻿using Newtonsoft.Json;

public class LocalJSONStateDeserializer<T> : AbstractLocalStateDeserializer<T> where T : IStateSnapshot
{
    private string jsonKey;

    public LocalJSONStateDeserializer(string jsonKey)
    {
        this.jsonKey = jsonKey;
    }

    public override T DeserializeState()
    {
        return JsonConvert.DeserializeObject<T>(this.jsonKey);
    }
}
