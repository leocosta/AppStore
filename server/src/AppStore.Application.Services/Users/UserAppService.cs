using AppStore.Application.DataContracts.Users;
using AppStore.Application.Services.Exceptions;
using AppStore.Domain.Common;
using AppStore.Domain.Users;
using System;

namespace AppStore.Application.Services.Users
{
    public class UserAppService : IUserAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UserAppService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public CreateUserResponse Create(CreateUserRequest request)
        {
            var response = new CreateUserResponse();
            try
            {
                var user = request.User.ToDomain(_userRepository);

                _userRepository.Add(user);
                _unitOfWork.Commit();

                response.User = user.ToDataContract();
            }
            catch (Exception ex)
            {
                response.Exception = ex;
            }

            return response;
        }

        public GetUserResponse Get(int id)
        {
            var response = new GetUserResponse();
            var user = _userRepository.Get(id);

            if (user == null)
                response.Exception = new ResourceNotFoundException("User not found.");

            response.User = user?.ToDataContract();
            
            return response;
        }

        public GetUserCreditCardsResponse GetCreditCards(int userId)
        {
            var response = new GetUserCreditCardsResponse();
            var user = _userRepository.Get(userId);

            if (user == null)
                response.Exception = new ResourceNotFoundException("User not found.");

            response.CreditCards = user?.CreditCards?.ToDataContract();

            return response;
        }
    }
}
