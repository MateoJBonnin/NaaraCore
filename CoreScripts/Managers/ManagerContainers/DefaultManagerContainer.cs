using Managers;

public class DefaultManagerContainer : AbstractManagerContainer<GameplayManager>
{
    public DefaultManagerContainer(GameplayManager manager) : base(manager)
    {
    }
}