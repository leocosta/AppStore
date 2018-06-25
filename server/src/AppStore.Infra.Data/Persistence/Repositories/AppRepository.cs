using System.Data.Entity;
using AppStore.Domain.Apps;

namespace AppStore.Infra.Data.Persistence.Repositories
{
    public class AppRepository : BaseRepository<App>, IAppRepository
    {
        public AppRepository(DbContext context)
            : base(context) { }
    
        public App Get(int id)
        {
            return base.FirstOrDefault(i => i.AppId == id);
        }
    }
}
