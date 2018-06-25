
using AppStore.Application.DataContracts.Payment;

namespace AppStore.Application.Services.Payment
{
    public interface IPaymentAppService
    {
        ProcessPaymentResponse Process(ProcessPaymentRequest request);
    }
}