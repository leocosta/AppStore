using AppStore.Domain.Common;

namespace AppStore.Domain.Users
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(int id);
        User Get(string email);
    }
}
