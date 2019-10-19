using System.Collections.Generic;

public abstract class AbstractUserInput : AbstractInputEntity
{
    protected AbstractUserInput()
    {
    }

    public AbstractUserInput(AbstractInputController inputController, List<EntityInputLink> entityInputLinks) : base(inputController, entityInputLinks)
    {
    }
}