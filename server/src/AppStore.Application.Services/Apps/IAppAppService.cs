using AppStore.Application.DataContracts.Users;

namespace AppStore.Application.Services.Apps
{
    public interface IAppAppService
    {
        GetAppResponse Get(int id);
        GetAppsResponse GetAll();
    }
}