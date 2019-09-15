public class ResponsabilityChainFirstHandlerProcessor<T> : AbstractResponsabilityChainProcessor<T>
{
    public override void ProcessHandler(RChainHandler<T> handler, T dataToHandle)
    {
        RChainHandler<T> currentHandler = handler;
        while (currentHandler != null)
        {
            if (handler.Handle(dataToHandle))
                return;
            else
                currentHandler = currentHandler.NextHandlerInChain;
        }
    }
}
