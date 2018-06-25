using AppStore.Domain.Users;
using System;

namespace AppStore.Domain.CreditCards
{
    public class CreditCard
    {
        public CreditCard() { }

        public CreditCard(User user, Guid instantBuyKey, CreditCardBrand brand, 
            string lastDigits, int expMonth, int expYear)
        {
            ValidateUser(user);
            ValidateBrand(brand);
            ValidateLastDigits(lastDigits);
            ValidateExpiration(expMonth, expYear);

            User = user;
            InstantBuyKey = instantBuyKey;
            Brand = brand;
            LastDigits = lastDigits;
            ExpMonth = expMonth;
            ExpYear = expYear;
        }

        private void ValidateUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");
        }

        private void ValidateExpiration(int expMonth, int expYear)
        {
            var cardDate = new DateTime(expYear, expMonth, 1);
            var isValid = cardDate.Date >= DateTime.Now.Date;
            if (!isValid)
                throw new ArgumentException("Invalid Expiration Date.");
        }

        private void ValidateLastDigits(string lastDigits)
        {
            var isValid = lastDigits.Length.Equals(4);
            if (!isValid)
                throw new ArgumentException("Invalid Brand.");
        }

        private void ValidateBrand(CreditCardBrand brand)
        {
            var exists = Enum.IsDefined(typeof(CreditCardBrand), brand);
            if (!exists)
                throw new ArgumentException("Invalid Brand.");
        }

        public int CreditCardId { get; private set; }
        public Guid InstantBuyKey { get; private set; }
        public CreditCardBrand Brand { get; private set; }
        public string LastDigits { get; private set; }
        public int ExpMonth { get; private set; }
        public int ExpYear { get; private set; }
        public virtual User User { get; private set; }
        public DateTime CreateDate { get; private set; } =  DateTime.Now;
        public DateTime? ModifyDate { get; private set; }
    }
}
