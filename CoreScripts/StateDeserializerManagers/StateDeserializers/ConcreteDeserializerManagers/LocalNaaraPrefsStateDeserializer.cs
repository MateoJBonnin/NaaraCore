public class LocalNaaraPrefsStateDeserializer : AbstractLocalStateDeserializer
{
    private LocalJSONStateDeserializer jsonDeserializer;

    public LocalNaaraPrefsStateDeserializer(string prefsKey)
    {
        this.jsonDeserializer = new LocalJSONStateDeserializer(NaaraPlayerPrefs.GetString(prefsKey));
    }

    public override StateSnapshot DeserializeState()
    {
        return this.jsonDeserializer.DeserializeState();
    }
}
