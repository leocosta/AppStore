import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

import * as _ from 'lodash';

@Injectable()
export class UrlBuilder {

  private url: string;
  private params: any;

  constructor(private args: any) { }

  static build(args: any): string {
    return new UrlBuilder(args)
      .configureUrl()
      .configureParams()
      .parseQuery()
      .configureODataOpts()
      .create();
  }

  private configureUrl() {
    const resource = this.args.resource.charAt(0) === '/'
        ? this.args.resource
        : ('/' + this.args.resource);
        this.url = environment.apiUrlBase + resource;

    return this;
  }

  private configureParams() {
    this.params = _.isObject(this.args.params) ? this.args.params : { id: this.args.params };

    return this;
  }

  private parseQuery() {
    if ('query' in this.params) {
      this.params.parsedQuery = _.isObject(this.params.query)
        ? this.serializeQuery(this.params.query)
        : this.params.query;
    }

    return this;
  }

  private serializeQuery(parsedQuery): string {
    return Object.keys(parsedQuery).map(function (key) {
      let value = _.get(parsedQuery, key);
      if (_.isNil(value)) {
        value = '';
      }
      return key + '=' + value;
    }).join('&');
  }

  private hasODataOpts() {
    if (!this.params) {
      return false;
    }

    return 'query' in this.params ||
      'select' in this.params ||
      'top' in this.params ||
      'skip' in this.params ||
      'count' in this.params ||
      'format' in this.params ||
      'groupby' in this.params;
  }

  private configureODataOpts() {

    if (this.url.indexOf('?') === -1 && this.hasODataOpts()) {
      this.url += '?';
    }

    if ('query' in this.params) {
      this.url += '${parsedQuery}';
    }

    if ('select' in this.params) {
      if (this.url.indexOf('&') === -1) {
        this.url += '&';
      }
      this.url += '$select=${select}';
    }

    if ('top' in this.params) {
      this.url += '&$top=${top}';
    }

    if ('skip' in this.params) {
      this.url += '&$skip=${skip}';
    }

    if ('groupby' in this.params) {
      this.url += '&$groupBy=${groupby}';
    }

    if ('orderby' in this.params) {
      this.url += '&$orderby=${orderby}';
    }

    if ('orderBy' in this.params) {
      this.url += '&$orderby=${orderBy}';
    }

    if ('format' in this.params) {
      this.url += '&$format=${format}';
    }

    if ('count' in this.params) {
      this.url += '&$count';
    }

    return this;
  }

  private create(): string {
    const compile = _.template(this.url);
    return compile(this.params);
  }
}
