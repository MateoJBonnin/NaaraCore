using System.Collections.Generic;

public class LocalUserInputContextData
{
    public List<AbstractEntityInputTrigger> abstractEntityInputTriggers;
    public AbstractGameInputTrigger abstractGameInputTrigger;

    public LocalUserInputContextData(List<AbstractEntityInputTrigger> abstractEntityInputTriggers, AbstractGameInputTrigger abstractGameInputTrigger)
    {
        this.abstractEntityInputTriggers = abstractEntityInputTriggers;
        this.abstractGameInputTrigger = abstractGameInputTrigger;
    }
}