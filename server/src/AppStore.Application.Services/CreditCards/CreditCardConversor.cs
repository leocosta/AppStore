using AppStore.DataContracts.CreditCards;
using System.Collections.Generic;
using System.Linq;
using CreditCardsDomain = AppStore.Domain.CreditCards;

namespace AppStore.Application.Services.Users
{
    public static class CreditCardConversor
    {
        public static CreditCard ToDataContract(this CreditCardsDomain.CreditCard creditCard)
        {
            return new CreditCard()
            {
                CreditCardId = creditCard.CreditCardId,
                ExpMonth = creditCard.ExpMonth,
                ExpYear = creditCard.ExpYear,
                Brand = (CreditCardBrand)creditCard.Brand,
                LastDigits = creditCard.LastDigits
            };
        }

        public static IEnumerable<CreditCard> ToDataContract(this IEnumerable<CreditCardsDomain.CreditCard> creditCards)
        {
            return creditCards.Select(s => s.ToDataContract());
        }
    }
}
