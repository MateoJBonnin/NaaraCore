using System;

[Serializable]
public abstract class AbstractSerializedStateSnapshot : IStateSnapshot
{
    public int serializedIndex;

    public AbstractSerializedStateSnapshot(int serializedIndex)
    {
        this.serializedIndex = serializedIndex;
    }
}