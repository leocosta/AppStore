import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { RequestBase, UrlBuilder } from '../../shared/services';

import { Credential } from './credential.model';
import { User } from '../../layout/user/shared/user.model';

@Injectable()
export class AuthenticationService extends RequestBase {


  constructor(public http: Http) {
    super(http);
  }

  post(credential: Credential): Observable<Response> {
    
    return this.http.post(UrlBuilder.build({
      resource: 'authentication',
    }), credential, this.options)
    .pipe(map(res => res.json()));
  }
  
  delete(): Observable<Response> {
    
    return this.http.delete(UrlBuilder.build({
      resource: 'authentication',
    }), this.options)
    .pipe(map(res => res.json()));
  }

  getMe(): Observable<User> {
    
    return this.http.get(UrlBuilder.build({
      resource: 'authentication',
    }), this.options)
    .pipe(map(res => User.build(res.json().user)));
  }

}
