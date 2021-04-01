public class LocalMemorySerializer : AbstractLocalStateSerializer
{
    public IStateSnapshot stateSnapshot;

    public override void SerializeState(IStateSnapshot stateSnapshot)
    {
        this.stateSnapshot = stateSnapshot;
    }
}
