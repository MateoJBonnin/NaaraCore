public class AbstractGameplayManagerContainer : AbstractGameplayManagerContainer<GameplayManager>
{
    public AbstractGameplayManagerContainer(GameplayManager gameplayManager) : base(gameplayManager)
    {
    }
}

public class AbstractGameplayManagerContainer<T> : AbstractManagerContainer<T> where T : class, GameplayManager
{
    public AbstractGameplayManagerContainer(T gameplayManager) : base(gameplayManager)
    {
    }
}