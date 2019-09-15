public class LocalMemorySerializer : AbstractLocalStateSerializer
{
    public StateSnapshot stateSnapshot;

    public override void SerializeState(StateSnapshot stateSnapshot)
    {
        this.stateSnapshot = stateSnapshot;
    }
}
