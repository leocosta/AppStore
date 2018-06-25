import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { RequestBase, UrlBuilder } from '../../../shared/services';

import { Order } from './order.model';

@Injectable()
export class OrderService extends RequestBase {

  constructor(public http: Http) {
    super(http);
  }

  post(order: Order): Observable<Order> {
    
    return this.http.post(UrlBuilder.build({
            resource: 'orders',
          }), order, this.options)
          .pipe(map(res => res.json()));
  }

}
