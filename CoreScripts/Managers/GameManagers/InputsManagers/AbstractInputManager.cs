using System;
using System.Collections.Generic;

public abstract class AbstractInputManager<T, W> : AutoInitManager where W : AbstractInputData where T : AbstractInputTrigger<W>
{
    private List<Action<W>> cachedEmptyActionList = new List<Action<W>>();
    private GenericDatabaseStructure<T, Action<W>> inputsDatabase;

    public AbstractInputManager()
    {
        this.inputsDatabase = new GenericDatabaseStructure<T, Action<W>>();
    }

    public void SubscribeToInput(T inputType, Action<W> inputAction)
    {
        this.inputsDatabase.RegisterData(inputType, inputAction);
    }

    public void RemoveSubscription(T inputType, Action<W> inputAction)
    {
        this.inputsDatabase.RemoveData(inputType, inputAction);
    }

    public void TriggerInput(T inputType, W data)
    {
        List<Action<W>> actionList = this.inputsDatabase.GetData(inputType);
        actionList = actionList != null ? actionList : this.cachedEmptyActionList;
        foreach (var action in actionList)
            action?.Invoke(data);
    }
}