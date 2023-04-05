namespace Queue
{
    public interface IQueueConsumer : IDisposable
    {
        void Consume();
    }
}
