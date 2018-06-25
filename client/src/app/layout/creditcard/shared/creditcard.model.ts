import { CreditCardBrand } from "./creditcard-brand.enum";

export class CreditCard {

  public creditCardId: number;
  public number: number;
  public holderName: string;
  public expMonth: number;
  public expYear: number;
  public securityCode: string;
  public brand: CreditCardBrand;
  public lastDigits: string;

  static transform<T>(data: any): T {
      if (data.constructor === Array) {
          return data.map(CreditCard.build) as T;
      }

      return CreditCard.build<T>(data);
  }

  static build<T>(data: any): T {
      const creditCard = new CreditCard();

      Object.assign(creditCard, data);

      return <any>creditCard as T;
  }

}
