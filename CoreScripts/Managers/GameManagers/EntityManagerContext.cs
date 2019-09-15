public abstract class EntityManagerContext<T> where T : EntityManager
{
    public abstract void ApplyContext(T entityManager);
}