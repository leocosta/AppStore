import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { RequestBase, UrlBuilder } from '../../../shared/services';

import { App } from './app.model';

@Injectable()
export class AppService extends RequestBase {

  constructor(public http: Http) {
    super(http);
  }

  getAll(): Observable<App[]> {

    return this.http.get(UrlBuilder.build({
          resource: 'apps'
        }), this.optionsNoPre)
        .pipe(map(res => App.transform<App[]>(res.json().apps)));
  }

  get(id: number): Observable<App> {

    return this.http.get(UrlBuilder.build({
          resource: 'apps/${id}',
          params: id
        }), this.optionsNoPre)
        .pipe(map(res => App.transform<App>(res.json().app)));
  }
}
