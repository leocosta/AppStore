namespace AppStore.Streaming.Producer
{
    public interface IPaymentProducer
    {
        void Produce(string message);
    }
}