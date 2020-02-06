public abstract class AbstractApplicationManager : ApplicationManager
{
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

    public void OnApplicationInitializerConfigured(ApplicationController applicationInitializer)
    {
    }

    public virtual void OnMainMenuLoaded()
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