import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { RequestBase, UrlBuilder } from '../../../shared/services';

import { User } from './user.model';
import { CreditCard } from '../../creditcard/shared/creditcard.model';

@Injectable()
export class UserService extends RequestBase {

  constructor(public http: Http) {
    super(http);
  }

  get(id: number): Observable<User> {

    return this.http.get(UrlBuilder.build({
          resource: 'users/${id}',
          params: id
        }), this.options)
        .pipe(map(res => User.transform<User>(res.json().user)));
  }

  post(user: User): Observable<User> {
    
    return this.http.post(UrlBuilder.build({
            resource: 'users',
          }), user, this.options)
          .pipe(map(res => res.json()));
  }

  getCreditCards(id: number): Observable<CreditCard[]> {

    return this.http.get(UrlBuilder.build({
          resource: 'users/${id}/creditcards',
          params: id
        }), this.options)
        .pipe(map(res => User.transform<CreditCard[]>(res.json().creditCards)));
  }

}
