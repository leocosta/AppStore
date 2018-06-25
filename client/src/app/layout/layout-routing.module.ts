import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LayoutComponent } from './layout.component';

const routes: Routes = [
    {
        path: '',
        component: LayoutComponent,
        children: [
            { path: '', redirectTo: 'home' },
            { path: 'home', loadChildren: './home/home.module#HomeModule' },
            { path: 'checkout', loadChildren: './checkout/checkout.module#CheckoutModule' }
        ]
    }
];

@NgModule({
    imports: [ RouterModule.forChild(routes) ],
    exports: [ RouterModule ]
})
export class LayoutRoutingModule {}
