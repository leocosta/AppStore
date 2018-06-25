using System;
using AppStore.Application.Services.Exceptions;
using AppStore.Domain.Apps;
using AppStore.Domain.Common;
using AppStore.Domain.Users;
using AppStore.Domain.CreditCards;
using AppStore.Infra.CrossCutting.Serialization;
using AppStore.Streaming.Producer;
using AppStore.Streaming.Handlers;
using AppStore.Domain.Orders;
using OrdersDataContract = AppStore.Application.DataContracts.Orders;

namespace AppStore.Application.Services.Users
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly IAppRepository _appRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPaymentProducer _paymentProducer;

        public OrderAppService(IUnitOfWork unitOfWork, 
            IOrderRepository orderRepository,
            IAppRepository appRepository,
            IUserRepository userRepository,
            IPaymentProducer paymentProducer)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _appRepository = appRepository;
            _userRepository = userRepository;
            _paymentProducer = paymentProducer;
        }

        public OrdersDataContract.CreateOrderResponse Create(OrdersDataContract.CreateOrderRequest request)
        {
            var response = new OrdersDataContract.CreateOrderResponse();
            try
            {
                var app = _appRepository.Get(request.Order.App.AppId.Value);
                if (app == null)
                {
                    response.Exception = new ResourceNotFoundException("App not found.");
                    return response;
                }

                var customer = _userRepository.Get(request.User.UserId.Value);
                if (customer == null)
                {
                    response.Exception = new ResourceNotFoundException("Customer not found.");
                    return response;
                }

                var creditCard = default(CreditCard);
                var creditCardRequested = request.Order.PaymentInfo.CreditCard;

                if (creditCardRequested.CreditCardId.HasValue)
                {
                    creditCard = customer.GetCreditCard(creditCardRequested.CreditCardId);
                    if (creditCard == null)
                    {
                        response.Exception = new NotAllowedOperationException("Credit card does not belong to this customer");
                        return response;
                    }
                }

                var order = new Order(customer, app);

                _orderRepository.Add(order);
                _unitOfWork.Commit();

                var processPaymentHandler = new ProcessPaymentHandler()
                {
                    OrderId = order.OrderId,
                    InstantBuyKey = creditCard?.InstantBuyKey,
                    Brand = (Streaming.Handlers.CreditCardBrand)creditCardRequested?.Brand,
                    Number = creditCardRequested?.Number,
                    ExpMonth = creditCardRequested?.ExpMonth,
                    ExpYear = creditCardRequested?.ExpYear,
                    HolderName = creditCardRequested?.HolderName,
                    SecurityCode = creditCardRequested?.SecurityCode,
                    SaveCreditCard = request.Order.PaymentInfo?.SaveCreditCard
                };

                _paymentProducer.Produce(processPaymentHandler.Serialize());
            }
            catch (Exception ex)
            {
                response.Exception = new InvalidResourceOperationException(ex.Message); ;
            }

            return response;
        }
    }
}
