public abstract class AbstractViewEntityManager : AbstractEntityManager, ViewEntityManager
{
    public ViewEntity ViewEntity
    {
        get
        {
            return this.viewEntity;
        }
        set
        {
            this.viewEntity = value;
            this.OnViewEntityConfigured(viewEntity);

        }
    }

    private ViewEntity viewEntity;

    public virtual void OnViewEntityConfigured(ViewEntity ViewEntity)
    {
    }

    public virtual void FixedUpdateManager()
    {
    }
}