public class LocalNaaraPrefsStateDeserializer<T> : AbstractLocalStateDeserializer<T> where T : IStateSnapshot
{
    private LocalJSONStateDeserializer<T> jsonDeserializer;
    private string prefsKey;

    public LocalNaaraPrefsStateDeserializer(string prefsKey)
    {
        this.prefsKey = prefsKey;
    }

    public override T DeserializeState()
    {
        this.jsonDeserializer = new LocalJSONStateDeserializer<T>(NaaraPlayerPrefs.GetString(prefsKey));
        return this.jsonDeserializer.DeserializeState();
    }
}
