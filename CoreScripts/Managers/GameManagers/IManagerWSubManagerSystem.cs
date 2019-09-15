namespace Managers
{
    public interface IManagerWSubManagerSystem<T> : IManager where T : SubManager
    {
        SubManagerSystem<T> SubManagerSystem { get; }
    }
}