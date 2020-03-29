public class AbstractViewEntityManager : AbstractEntityManager, ViewEntityManager
{
    public AbstractViewEntity ViewEntity
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

    private AbstractViewEntity viewEntity;

    public void OnViewEntityConfigured(AbstractViewEntity ViewEntity)
    {
    }
}