using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace AppStore.Application.DataContracts.Users
{
    [DataContract]
    public class ValidatableUser : User, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
                yield return new ValidationResult("Name is required.");

            if (string.IsNullOrWhiteSpace(SSN))
                yield return new ValidationResult("SSN is required.");

            if (!Enum.IsDefined(typeof(Gender), Gender))
                yield return new ValidationResult("Invalid gender.");

            if (Address == null)
                yield return new ValidationResult("Address is required.");

            if (string.IsNullOrWhiteSpace(Email))
                yield return new ValidationResult("Email is required.");

            var emailValidator = new EmailAddressAttribute();
            if (!emailValidator.IsValid(Email))
                yield return new ValidationResult("Invalid email.");

            if (string.IsNullOrWhiteSpace(Password))
                yield return new ValidationResult("Password is required.");
        }
    }
}
