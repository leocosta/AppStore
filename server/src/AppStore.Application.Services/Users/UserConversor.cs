using AppStore.Application.DataContracts.Users;
using UsersDomain = AppStore.Domain.Users;

namespace AppStore.Application.Services.Users
{
    public static class UserConversor
    {
        public static UsersDomain.User ToDomain(this User user, UsersDomain.IUserRepository userRepository)
        {
            return new UsersDomain.User(user.Email, user.Password, userRepository)
            {
                Name = user.Name,
                Birthdate = user.Birthdate,
                Gender = (UsersDomain.Gender)user.Gender,
                SSN = user.SSN,
                Address = user.Address.ToDomain()
            };
        }

        public static User ToDataContract(this UsersDomain.User user)
        {
            return new User()
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = user.Name,
                Birthdate = user.Birthdate,
                Gender = (Gender)user.Gender,
                SSN = user.SSN,
                Address = user.Address.ToDataContract()
            };
        }
    }
}
