namespace AppStore.Domain.Users
{
    public interface IUser
    {
        int UserId { get; set; }

        void ValidateAccess(string password);
    }
}