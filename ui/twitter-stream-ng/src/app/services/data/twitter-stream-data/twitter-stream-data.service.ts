import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';
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

  getTop10HashTagsByHourWindow(hourWindow?: number): Observable<string[]> {

    const url = `${environment.twitterStreamWebApiRootUrl}/hash-tags/rpc/get-top-10`;

    const params: any = this.createHttpParams({
      hourWindow
    });

    return this._httpClient
      .get<string[]>(url, { params, observe: 'response' })
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
