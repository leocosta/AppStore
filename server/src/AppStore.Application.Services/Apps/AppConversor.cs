using AppStore.Application.DataContracts.Apps;
using System.Collections.Generic;
using System.Linq;
using AppsDomain = AppStore.Domain.Apps;

namespace AppStore.Application.Services.Apps
{
    public static class AppConversor
    {
        public static AppsDomain.App ToDomain(this App app)
        {
            return new AppsDomain.App()
            {
                Name = app.Name,
                Developer = app.Developer,
                Description = app.Description,
                Price = app.Price,
                ThumbUrl = app.ThumbUrl
            };
        }

        public static App ToDataContract(this AppsDomain.App app)
        {
            return new App()
            {
                AppId = app.AppId,
                Name = app.Name,
                Developer = app.Developer,
                Description = app.Description,
                Price = app.Price,
                ThumbUrl = app.ThumbUrl
            };
        }

        public static IEnumerable<App> ToDataContract(this IEnumerable<AppsDomain.App> apps)
        {
            return apps.Select(i => i.ToDataContract());
        }
    }
}
