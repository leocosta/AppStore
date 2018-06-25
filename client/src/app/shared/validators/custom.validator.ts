
// Angular
import { FormControl } from '@angular/forms';

export class CustomValidators {

    static email(control: FormControl): ValidationResult {
        const regex = /^[A-Za-z0-9_\-\.]+@[A-Za-z0-9_\-\.]{2,}\.[A-Za-z0-9]{2,}(\.[A-Za-z0-9])?/;
        const value = control.value || '';
        if (value.length <= 5 || !regex.test(value)) {
            return { 'email': true };
        }

        return null;
    }

    static SSN(control: FormControl): ValidationResult {
        const value = control.value || '';
        if (CustomValidators.isValidSSN(value)) {
            return null;
        }

        return { 'SSN': false };
    }

    static zipCode(control: FormControl): ValidationResult {

        const value = control.value || '';
        if (CustomValidators.isValidZipCode(value)) {
            return null;
        }

        return { 'cep': false };
    }

    static isValidZipCode(value: string): boolean {
        const pattern = /^\d{5}-?\d{3}$/;
        const newValue = value.replace('-', '');

        return pattern.test(newValue);
    }

    static isValidSSN(ssn: string): boolean {

        let sum = 0;
        let remainder;
        let number = ssn.replace(/[^\d]+/g, '');

        let allEqual = true;
        for (var i = 0; i < number.length - 1; i++) {
            if (number[i] != number[i + 1])
                allEqual = false;
        }
        if (allEqual)
            return false;

        for (i = 1; i <= 9; i++)
            sum = sum + parseInt(number.substring(i - 1, i)) * (11 - i);
        remainder = (sum * 10) % 11;

        if ((remainder == 10) || (remainder == 11))
            remainder = 0;
        if (remainder != parseInt(number.substring(9, 10)))
            return false;

        sum = 0;
        for (i = 1; i <= 10; i++)
            sum = sum + parseInt(number.substring(i - 1, i)) * (12 - i); remainder = (sum * 10) % 11;

        if ((remainder == 10) || (remainder == 11))
            remainder = 0;
        if (remainder != parseInt(number.substring(10, 11)))
            return false;

        return true;
      
    }
}

interface ValidationResult {
    [key: string]: boolean;
}
