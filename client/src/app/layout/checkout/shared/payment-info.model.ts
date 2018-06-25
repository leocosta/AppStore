import { CreditCard } from "../../creditcard/shared/creditcard.model";

export class PaymentInfo {
    creditCard: CreditCard = new CreditCard;
    saveCreditCard: boolean;
}