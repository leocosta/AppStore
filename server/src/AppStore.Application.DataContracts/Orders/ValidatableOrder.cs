using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Orders
{
    [DataContract]
    public class ValidatableOrder : Order, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IsValidApp())
                yield return new ValidationResult("Invalid app info.");

            if (!IsValidPaymentInfo())
                yield return new ValidationResult("Invalid payment info.");
        }

        private bool IsValidApp() => App?.IsValid() ?? false;
        private bool IsValidPaymentInfo() => PaymentInfo?.IsValid() ?? false;
    }
}
