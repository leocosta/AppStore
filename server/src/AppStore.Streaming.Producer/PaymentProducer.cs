using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System.Collections.Generic;
using System.Text;

namespace AppStore.Streaming.Producer
{
    public class PaymentProducer : IPaymentProducer
    {
        private const string ProcessPaymentTopic = "appstore-process-payment";

        private static Dictionary<string, object> brokerConfig =
            new Dictionary<string, object>()
            {
                { "bootstrap.servers", "localhost:9092" }
            };

        private static StringSerializer serializer = new StringSerializer(Encoding.UTF8);

        public void Produce(string message)
        {
            using (var producer = new Producer<Null, string>(brokerConfig, null, serializer))
            {
                producer.ProduceAsync(ProcessPaymentTopic, null, message)
                    .GetAwaiter()
                    .GetResult();

                producer.Flush(10);
            }
        }
    }
}
