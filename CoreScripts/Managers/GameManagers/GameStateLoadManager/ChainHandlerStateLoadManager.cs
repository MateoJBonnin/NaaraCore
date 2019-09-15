public abstract class ChainHandlerStateLoadManager : RChainHandler<StateSnapshot>
{
    private AbstractStateLoadManager stateLoadManager;

    protected ChainHandlerStateLoadManager(AbstractStateLoadManager stateLoadManager)
    {
        this.stateLoadManager = stateLoadManager;
    }
}