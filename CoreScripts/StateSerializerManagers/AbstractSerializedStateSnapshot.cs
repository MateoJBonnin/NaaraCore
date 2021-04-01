using System;

[Serializable]
public abstract class AbstractSerializedStateSnapshot : StateSnapshot
{
    public int serializedIndex;

    public AbstractSerializedStateSnapshot(int serializedIndex)
    {
        this.serializedIndex = serializedIndex;
    }
}