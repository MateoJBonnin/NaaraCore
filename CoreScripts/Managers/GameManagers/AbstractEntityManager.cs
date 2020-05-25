public abstract class AbstractEntityManager : EntityManager
{
    public ApplicationController ApplicationController
    {
        get
        {
            return this.applicationController;
        }
        set
        {
            this.applicationController = value;
            this.OnApplicationInitializerConfigured(this.applicationController);

        }
    }

    public GameplayController GameplayController
    {
        get
        {
            return this.gameplayController;
        }
        set
        {
            this.gameplayController = value;
            this.OnGameplayInitializerConfigured(this.gameplayController);

        }
    }

    private ApplicationController applicationController;
    private GameplayController gameplayController;

    public void OnApplicationInitializerConfigured(ApplicationController applicationController)
    {
    }

    public void OnGameplayInitializerConfigured(GameplayController gameplayController)
    {
    }

    /// <summary>
    /// Will be called after the Entity has been assigned for the first time.
    /// Used to initialize the manager, self references, sub managers, etc. Other managers of the Entity may not be yet available / ready.
    /// </summary>
    public virtual void OnReady()
    {
    }

    /// <summary>
    /// Will be called after all the sub managers for this entity are ready.
    /// Used to set references of others sub managers as they will be available in this point.
    /// </summary>
    public virtual void OnInit()
    {
    }

    public virtual void UpdateManager()
    {
    }
}