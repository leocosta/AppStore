using AppStore.Application.DataContracts.Users;
using AppStore.Application.Services.Exceptions;
using AppStore.Domain.Apps;
using AppStore.Domain.Common;

namespace AppStore.Application.Services.Apps
{
    public class AppAppService : IAppAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppRepository _appRepository;

        public AppAppService(IUnitOfWork unitOfWork, IAppRepository appRepository)
        {
            _unitOfWork = unitOfWork;
            _appRepository = appRepository;
        }

        public GetAppsResponse GetAll()
        {
            var response = new GetAppsResponse();
            var apps = _appRepository.GetAll();

            response.Apps = apps.ToDataContract();

            return response;
        }

        public GetAppResponse Get(int id)
        {
            var response = new GetAppResponse();
            var app = _appRepository.Get(id);

            if (app == null)
                response.Exception = new ResourceNotFoundException("App not found.");

            response.App = app?.ToDataContract();

            return response;
        }
    }
}
