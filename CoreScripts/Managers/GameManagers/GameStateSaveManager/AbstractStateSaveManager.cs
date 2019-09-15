using System.Collections.Generic;

public abstract class AbstractStateSaveManager : SubManager
{
    public abstract StateSnapshot GetStateSnapshot();
}