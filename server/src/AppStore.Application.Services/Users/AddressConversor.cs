using AppStore.Application.DataContracts.Users;
using UsersDomain = AppStore.Domain.Users;

namespace AppStore.Application.Services.Users
{
    public static class AddressConversor
    {
        public static UsersDomain.Address ToDomain(this Address address)
        {
            return new UsersDomain.Address()
            {
                Street = address.Street,
                Number = address.Number,
                Complement = address.Complement,
                District = address.District,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode
            };
        }

        public static Address ToDataContract(this UsersDomain.Address address)
        {
            return new Address()
            {
                Street = address.Street,
                Number = address.Number,
                Complement = address.Complement,
                District = address.District,
                City = address.City,
                State = address.State,
                ZipCode = address.ZipCode
            };
        }
    }
}
