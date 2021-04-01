public class LocalNaaraPrefsStateSerializer : AbstractLocalStateSerializer
{
    private LocalJSONStateSerializer jsonSerializer;
    private string prefsKey;

    public LocalNaaraPrefsStateSerializer(string prefsKey)
    {
        this.prefsKey = prefsKey;
        this.jsonSerializer = new LocalJSONStateSerializer();
    }

    public override void SerializeState(IStateSnapshot stateSnapshot)
    {
        this.jsonSerializer.SerializeState(stateSnapshot);
        NaaraPlayerPrefs.SetString(prefsKey, this.jsonSerializer.jsonGameSerializedString);
    }
}
