using Managers;

public abstract class AbstractGameplayManagerWSubManagers<SubManagerContainerType, SubManagerType> : AbstractGameplayManager where SubManagerType : Manager where SubManagerContainerType : AbstractManagerContainer<SubManagerType>
{
    protected SubManagerSystem<SubManagerContainerType, SubManagerType> subManagerSystem;

    public T GetSubManager<T>() where T : SubManagerType
    {
        return this.subManagerSystem.GetManager<T>();
    }
}