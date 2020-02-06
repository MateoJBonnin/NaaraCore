public class LocalNaaraPrefsStateDeserializer<T> : AbstractLocalStateDeserializer<T> where T : StateSnapshot
{
    private LocalJSONStateDeserializer<T> jsonDeserializer;

    public LocalNaaraPrefsStateDeserializer(string prefsKey)
    {
        this.jsonDeserializer = new LocalJSONStateDeserializer<T>(NaaraPlayerPrefs.GetString(prefsKey));
    }

    public override T DeserializeState()
    {
        return this.jsonDeserializer.DeserializeState();
    }
}
