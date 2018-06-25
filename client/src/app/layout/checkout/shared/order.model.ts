import { App } from "../../apps/shared/app.model";
import { PaymentInfo } from "./payment-info.model";

export class Order {
    app: App;
    paymentInfo: PaymentInfo = new PaymentInfo;
}