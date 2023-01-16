import { Component, OnDestroy, OnInit } from '@angular/core';
import { TwitterStreamDataService } from './services/data/twitter-stream-data/twitter-stream-data.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {

  top10HashTags: string[] = new Array<string>()

  totalTweets: number = 0;

  interval: NodeJS.Timer | undefined;

  constructor(private _twitterStreamDataService: TwitterStreamDataService) { }

  ngOnInit(): void {
    this._twitterStreamDataService.getTop10HashTagsByHourWindow(1)
      .subscribe((top10HashTags: string[]) => {
        this.top10HashTags = top10HashTags
      });

    this._twitterStreamDataService.getTotalNumberOfTweets()
      .subscribe((totalTweets: number) => {
        this.totalTweets = totalTweets
      });

    this.interval = setInterval(() => {
      this._twitterStreamDataService.getTop10HashTagsByHourWindow(1)
        .subscribe((top10HashTags: string[]) => {
          this.top10HashTags = top10HashTags
        });

      this._twitterStreamDataService.getTotalNumberOfTweets()
        .subscribe((totalTweets: number) => {
          this.totalTweets = totalTweets
        }); // api call
    }, 30000);
  }

  ngOnDestroy() {
    if (this.interval) {
      clearInterval(this.interval);
    }
  }
}
