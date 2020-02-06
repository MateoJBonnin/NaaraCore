public class DefaultGameplayManagerContainer : AbstractGameplayManagerContainer
{
    public DefaultGameplayManagerContainer(GameplayManager gameplayManager) : base(gameplayManager)
    {
    }
}

public class DefaultGameplayManagerContainer<T> : AbstractGameplayManagerContainer<T> where T : class, GameplayManager
{
    public DefaultGameplayManagerContainer(T gameplayManager) : base(gameplayManager)
    {
    }
}