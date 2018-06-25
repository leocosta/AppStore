using AppStore.Application.DataContracts.Payment;
using AppStore.Application.Services.Payment;
using AppStore.Infra.CrossCutting.Serialization;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using AppStore.DataContracts.CreditCards;

namespace AppStore.Streaming.Consumer
{
    public class PaymentConsumer
    {
        private const string ProcessPaymentTopic = "appstore-process-payment";

        private readonly static Dictionary<string, object> brokerConfig =
            new Dictionary<string, object>()
            {
                { "group.id", "appstore-payment-consumer" },
                { "bootstrap.servers", "localhost:9092" },
                { "enable.auto.commit", "false" }
            };

        private readonly static StringDeserializer deserializer = new StringDeserializer(Encoding.UTF8);
        private readonly IPaymentAppService _paymentAppService;

        public PaymentConsumer(IPaymentAppService paymentAppService)
        {
            _paymentAppService = paymentAppService;
        }

        public void Listen(Action<string> message)
        {
            using (var consumer = new Consumer<Null, string>(brokerConfig, null, deserializer))
            {
                consumer.Subscribe(ProcessPaymentTopic);
                consumer.OnMessage += (_, msg) => {
                    message(msg.Value);

                    var value = msg.Value;
                    var handler = value.Deserialize<Handlers.ProcessPaymentHandler>();

                    var request = new ProcessPaymentRequest
                    {
                        OrderId = handler.OrderId,
                        InstantBuyKey = handler.InstantBuyKey,
                        Brand = (CreditCardBrand)handler.Brand,
                        Number = handler.Number,
                        ExpMonth = handler.ExpMonth,
                        ExpYear = handler.ExpYear,
                        HolderName = handler.HolderName,
                        SecurityCode = handler.SecurityCode,
                        SaveCreditCard = handler.SaveCreditCard
                    };
                    _paymentAppService.Process(request);
                };

                var cancelled = false;
                Console.CancelKeyPress += (_, e) =>
                {
                    cancelled = true;
                    e.Cancel = true;
                };

                while (!cancelled)
                    consumer.Poll(-1);
            }
        }
    }
}
