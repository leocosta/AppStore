import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CheckoutRoutingModule } from './checkout-routing.module';
import { CheckoutComponent } from './checkout.component';
import { PageHeaderModule } from './../../shared';
import { ReactiveFormsModule } from '@angular/forms';
import { NumberOnlyDirective } from '../../shared/directives';

@NgModule({
    imports: [ CommonModule, ReactiveFormsModule, CheckoutRoutingModule, PageHeaderModule ],
    declarations: [ CheckoutComponent, NumberOnlyDirective ]
})
export class CheckoutModule {}
