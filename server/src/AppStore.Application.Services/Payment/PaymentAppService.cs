using AppStore.Domain.Common;
using AppStore.Domain.Orders;
using AppStore.Application.DataContracts.Payment;
using AppStore.Application.Services.Exceptions;
using AppStore.Domain.Notifications;
using System;
using AppStore.Infra.CrossCutting.Logging;

namespace AppStore.Application.Services.Payment
{
    public class PaymentAppService : IPaymentAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentService _paymentService;
        private readonly INotificationService _notificationService;

        public PaymentAppService(IUnitOfWork unitOfWork, 
            IOrderRepository orderRepository,
            IPaymentService paymenyService,
            INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _paymentService = paymenyService;
            _notificationService = notificationService;
        }

        public ProcessPaymentResponse Process(ProcessPaymentRequest request)
        {
            var response = new ProcessPaymentResponse();

            var order = _orderRepository.Get(request.OrderId);
            if (order == null)
            {
                response.Exception = new ResourceNotFoundException("Order not found.");
                return response;
            }

            try
            {
                var paymentInfo = new PaymentInfo()
                {
                    InstantBuyKey = request.InstantBuyKey,
                    CreditCardBrand = (Domain.CreditCards.CreditCardBrand)request.Brand,
                    CreditCardNumber = request.Number,
                    ExpMonth = request.ExpMonth,
                    ExpYear = request.ExpYear,
                    HolderName = request.HolderName,
                    SecurityCode = request.SecurityCode,
                    Amount = (long)order.Price,
                    SaveCreditCard = request.SaveCreditCard ?? false
                };

                var paymentResult = _paymentService.CreateTransaction(paymentInfo);
                if (paymentResult.Success())
                {
                    order.PaymentReceived(paymentResult);
                    _unitOfWork.Commit();

                    _notificationService.SendPaymentReceived(order);
                }
                else
                {
                    order.PaymentReview();
                    _unitOfWork.Commit();

                    _notificationService.SendPaymentReview(order);
                }
            }
            catch (Exception ex)
            {
                response.Exception = ex;

                Logger.Error("Process Payment", ex);
            }

            return response;
        }
    }
}
