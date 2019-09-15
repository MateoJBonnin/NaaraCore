public class ResponsabilityChainAllChainHandlerProcessor<T> : AbstractResponsabilityChainProcessor<T>
{
    private const int WATCHDOG_PROCESS_WARNING = 10000;
    private const string WATCHDOG_PROCESS_WARNING_MESSAGE = "RESPONSABILITY CHAIN WARNING MESSAGE, INFINITE LOOP";
    private int watchdogCounter = 0;

    public override void ProcessHandler(RChainHandler<T> handler, T dataToHandle)
    {
        RChainHandler<T> currentHandler = handler;
        while (currentHandler != null)
        {
            handler.Handle(dataToHandle);
            currentHandler = currentHandler.NextHandlerInChain;
            watchdogCounter++;
            if (watchdogCounter > WATCHDOG_PROCESS_WARNING)
            {
                NaaraLogger.LogError(WATCHDOG_PROCESS_WARNING_MESSAGE);
                return;
            }
        }
    }
}
