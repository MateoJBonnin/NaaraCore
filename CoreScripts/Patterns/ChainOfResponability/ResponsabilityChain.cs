public class ResponsabilityChain<T> where T : RChainHandler<T>
{
    private AbstractResponsabilityChainProcessor<T> chainProcessor;

    public ResponsabilityChain(AbstractResponsabilityChainProcessor<T> chainProcessor)
    {
        this.chainProcessor = chainProcessor;
    }

    public void Handle(RChainHandler<T> chainHandler, T dataToHandle)
    {
        this.chainProcessor.ProcessHandler(chainHandler, dataToHandle);
    }
}
