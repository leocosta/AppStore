using AppStore.API.Filters;
using AppStore.API.Helpers;
using AppStore.Application.DataContracts.Orders;
using AppStore.Application.Services.Users;
using System.Net;
using System.Net.Http;

namespace AppStore.API.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderAppService _orderAppService;

        public OrdersController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        // POST api/orders
        [RequestValidate]
        public HttpResponseMessage Post(ValidatableOrder order)
        {
            var request = new CreateOrderRequest() {
                User = UserSession,
                Order = order
            };

            var response = _orderAppService.Create(request);

            return Request.BuildResponse(HttpStatusCode.Created, response);
        }
    }
}
