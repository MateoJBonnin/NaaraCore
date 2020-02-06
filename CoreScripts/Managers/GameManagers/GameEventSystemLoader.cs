public class GameEventSystemLoader : AbstractGameplayManager
{
    public GameplayEventSystem gameEventSystem;

    public GameEventSystemLoader()
    {
        this.gameEventSystem = new GameplayEventSystem();
        this.gameEventSystem.Init();
    }
}