using System;
using System.Collections.Generic;
using AppStore.Infra.CrossCutting.Encryption.Extension;
using AppStore.Domain.CreditCards;
using System.Linq;

namespace AppStore.Domain.Users
{
    public class User : IUser
    {
        public User() { }
        public User(string email, string password, IUserRepository userRepository)
        {
            ValidateIfExists(email, userRepository);
            ValidatePassword(password);

            Email = email;
            SetPassword(password);
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public Gender Gender { get; set; }
        public virtual Address Address { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; internal set; }
        public string Password { get; internal set; }
        public virtual ICollection<CreditCard> CreditCards { get; set; } = new List<CreditCard>();
        public int AuthAttempt { get; private set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public DateTime? ModifyDate { get; set; }
        public virtual void ValidateAccess(string password) => PasswordMatch(password);

        internal void AddCreditCard(Guid instantBuyKey, CreditCardBrand brand,
            string lastDigits, int expMonth, int expYear)
        {
            var card = new CreditCard(this, instantBuyKey, brand, lastDigits, expMonth, expYear);
            this.CreditCards.Add(card);
        }

        private void PasswordMatch(string password)
        {
            const int attemptsLimit = 10;

            if (!Password.Equals(password.Encrypt()))
            {
                AuthAttempt++;

                var message = AuthAttempt > attemptsLimit
                    ? "Password attempts exceeded. Access suspended."
                    : "User not authorized.";

                throw new UnauthorizedException(message);
            }

            AuthAttempt = 0;
        }

        private void ValidateIfExists(string email, IUserRepository _userRepository)
        {
            if (_userRepository.Any(a => a.UserId != this.UserId && a.Email.Equals(email)))
                throw new InvalidOperationException("Email account is already registered.");
        }

        public CreditCard GetCreditCard(int? creditCardId) => 
            CreditCards.FirstOrDefault(i => i.CreditCardId.Equals(creditCardId));

        private void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Invalid password.");
        }

        private void SetPassword(string password) => this.Password = password.Encrypt();

    }
}
