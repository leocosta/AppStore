import { Component, OnInit, OnDestroy } from '@angular/core';
import { routerTransition } from '../../router.animations';
import { App, AppService } from '../apps/shared';
import { Subscription } from 'rxjs';
import { Router, ActivatedRoute } from '@angular/router';
import { UserService } from '../user/shared';
import { CreditCard, CreditCardBrand } from '../creditcard/shared';
import { UserSession } from '../../shared/services';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { Order, OrderService } from './shared';

@Component({
    selector: 'app-checkout',
    templateUrl: './checkout.component.html',
    styleUrls: [ './checkout.component.scss' ],
    animations: [ routerTransition() ],
    providers: [ AppService, UserService, OrderService ]
})
export class CheckoutComponent implements OnInit, OnDestroy {
    private routeSubscribe: Subscription;

    order: Order = new Order;    
    creditCards: Array<CreditCard>;
    checkoutForm: FormGroup;

    constructor(private router: Router,
        private route: ActivatedRoute,
        private userSession: UserSession,
        private appService: AppService,
        private userService: UserService,
        private orderService: OrderService) {

        this.routeSubscribe = this.route.params.subscribe(params => {
            this.loadApp(params.id);
            this.loadCreditCards();
        });
    }

    ngOnInit() {
        this.configureForm(true);
    }

    ngOnDestroy() {
        this.routeSubscribe.unsubscribe();
    }
    
    purchase() {
        if (!this.checkoutForm.valid) {
            alert('Você deve preencher o formulário corretamente.');
            return;
        }

        this.order.paymentInfo.creditCard.brand = CreditCardBrand.Visa;
        this.orderService.post(this.order)
          .subscribe(() => {
                  alert('Pedido criado com sucesso!');
                  this.router.navigate(['home']);
              }, () => alert('Erro ao criar Pedido.'));
    }

    configureValidation() {
        this.configureForm(false);
    }

    private configureForm(requireCardFields: boolean): void {

        let validators = Validators.compose([]);
        if (requireCardFields) {
            validators = Validators.compose([Validators.required]);
        }

        this.checkoutForm = new FormGroup({
            'creditCard.creditCardId': new FormControl(this.order.paymentInfo.creditCard.creditCardId),
            'creditCard.brand': new FormControl(),
            'creditCard.number': new FormControl(null, validators),
            'creditCard.expMonth': new FormControl(null, validators),
            'creditCard.expYear': new FormControl(null, validators),
            'creditCard.securityCode': new FormControl(null, validators),
            'creditCard.holderName': new FormControl(null, validators),
            'saveCreditCard': new FormControl()
        });
    }
    
    private loadCreditCards() {
        this.userService.getCreditCards(this.userSession.userId)
            .subscribe((creditCards: Array<CreditCard>) => this.creditCards = creditCards);
    }

    private loadApp(id: number) {
        this.appService.get(id)
            .subscribe((app: App) => this.order.app = app);
    }

}
