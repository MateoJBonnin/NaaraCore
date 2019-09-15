public abstract class RChainHandler<T>
{
    public RChainHandler<T> NextHandlerInChain
    {
        get
        {
            return this.nextChainHandler;
        }
        private set { }
    }

    private RChainHandler<T> nextChainHandler;

    public RChainHandler(RChainHandler<T> nextChainHandler)
    {
        this.nextChainHandler = nextChainHandler;
    }

    public RChainHandler()
    {
    }

    public abstract bool Handle(T toHandle);
}