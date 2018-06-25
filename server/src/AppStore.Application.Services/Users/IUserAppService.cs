using AppStore.Application.DataContracts.Users;

namespace AppStore.Application.Services.Users
{
    public interface IUserAppService
    {
        CreateUserResponse Create(CreateUserRequest request);
        GetUserResponse Get(int id);
        GetUserCreditCardsResponse GetCreditCards(int userId);
    }
}