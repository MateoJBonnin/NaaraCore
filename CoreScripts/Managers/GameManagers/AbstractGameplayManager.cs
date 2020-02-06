public abstract class AbstractGameplayManager : GameplayManager
{
    public GameplayController GameplayController
    {
        get
        {
            return this.gameplayInitializer;
        }
        set
        {
            this.gameplayInitializer = value;
            this.OnGameplayInitializerConfigured(this.gameplayInitializer);

        }
    }

    public ApplicationController ApplicationController
    {
        get
        {
            return this.applicationInitializer;
        }
        set
        {
            this.applicationInitializer = value;
            this.OnApplicationInitializerConfigured(this.applicationInitializer);

        }
    }

    private ApplicationController applicationInitializer;
    private GameplayController gameplayInitializer;

    public void OnApplicationInitializerConfigured(ApplicationController applicationInitializer)
    {
    }

    public virtual void OnGameplayInitializerConfigured(GameplayController gameplayInitializer)
    {
    }

    public virtual void OnReady()
    {
    }

    public virtual void OnInit()
    {
    }

    public virtual void UpdateManager()
    {
    }
}