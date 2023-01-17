import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
import { HashTag } from 'src/app/models/hash-tag';
import { Tweet } from 'src/app/models/tweet';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TwitterStreamDataService {

  constructor(private _httpClient: HttpClient) { }

  getTotalNumberOfTweets(): Observable<number> {

    const url = `${environment.twitterStreamWebApiRootUrl}/tweets/rpc/get-total-tweets`;

    return this._httpClient
      .get<number>(url, { observe: 'response' })
      .pipe(
        map((response: any) => response.body)
      );
  }

  getMostRecentTweets(count?: number): Observable<Tweet[]> {

    const url = `${environment.twitterStreamWebApiRootUrl}/tweets`;

    const params: any = this.createHttpParams({
      count
    });

    return this._httpClient
      .get<Tweet[]>(url, { params, observe: 'response' })
      .pipe(
        map((response: any) => response.body)
      );
  }

  getTop10HashTagsByHourWindow(hourWindow?: number): Observable<HashTag[]> {

    const url = `${environment.twitterStreamWebApiRootUrl}/hash-tags/rpc/get-top-10`;

    const params: any = this.createHttpParams({
      hourWindow
    });

    return this._httpClient
      .get<HashTag[]>(url, { params, observe: 'response' })
      .pipe(
        map((response: any) => response.body)
      );
  }

  protected createHttpParams(params: any): HttpParams {
    let httpParams: HttpParams = new HttpParams();
    Object.keys(params).forEach(param => {
      if (params[param]) {
        httpParams = httpParams.set(param, params[param]);
      }
    });

    return httpParams;
  }

}
