using System;
using System.Collections.Generic;

public abstract class AbstractInputManager<T, W> : AbstractGameplayManager where W : AbstractInputData where T : AbstractInputTrigger<W>
{
    private List<Action<W>> cachedEmptyActionList = new List<Action<W>>();
    private GenericDatabase<T, Action<W>> inputsDatabase;
    private Dictionary<int, T> indexToTrigger;

    public AbstractInputManager()
    {
        this.inputsDatabase = new GenericDatabase<T, Action<W>>();
        this.indexToTrigger = new Dictionary<int, T>();
    }

    public void SubscribeToInput(T inputType, Action<W> inputAction)
    {
        this.inputsDatabase.RegisterData(inputType, inputAction);
        this.indexToTrigger[indexToTrigger.Count - 1] = inputType;
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

    public void TriggerInput(int inputTypeIndex, W data)
    {
        if (this.indexToTrigger.ContainsKey(inputTypeIndex))
        {
            T inputType = this.indexToTrigger[inputTypeIndex];
            this.TriggerInput(inputType, data);
        }
    }
}