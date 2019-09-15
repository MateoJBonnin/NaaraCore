public abstract class AbstractResponsabilityChainProcessor<T>
{
    public abstract void ProcessHandler(RChainHandler<T> handler, T dataToHandle);
}
