using AppStore.Domain.Common;

namespace AppStore.Domain.Apps
{
    public interface IAppRepository : IRepository<App>
    {
        App Get(int id);
    }
}
