using System.Collections.Generic;

public abstract class AbstractStateSaveManager : AutoInitManager
{
    public abstract StateSnapshot GetStateSnapshot();
}