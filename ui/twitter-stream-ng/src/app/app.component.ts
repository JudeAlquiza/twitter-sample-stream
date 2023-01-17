import { Component, OnDestroy, OnInit } from '@angular/core';
import { TwitterStreamDataService } from './services/data/twitter-stream-data/twitter-stream-data.service';
import { HashTag } from './models/hash-tag';
import { Tweet } from './models/tweet';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {

  top10HashTags: HashTag[] = new Array<HashTag>();

  mostRecentTweets: Tweet[] = new Array<Tweet>();

  totalTweets: number = 0;

  interval: NodeJS.Timer | undefined;

  constructor(private _twitterStreamDataService: TwitterStreamDataService) { }

  ngOnInit(): void {
    this._twitterStreamDataService.getMostRecentTweets(15)
      .subscribe((mostRecentTweets: Tweet[]) => {
        this.mostRecentTweets = mostRecentTweets;
      });

    this._twitterStreamDataService.getTop10HashTagsByHourWindow(1)
      .subscribe((top10HashTags: HashTag[]) => {
        this.top10HashTags = top10HashTags;
      });

    this._twitterStreamDataService.getTotalNumberOfTweets()
      .subscribe((totalTweets: number) => {
        this.totalTweets = totalTweets;
      });

    this.interval = setInterval(() => {
      this._twitterStreamDataService.getMostRecentTweets(15)
        .subscribe((mostRecentTweets: Tweet[]) => {
          this.mostRecentTweets = mostRecentTweets;
        });

      this._twitterStreamDataService.getTop10HashTagsByHourWindow(1)
        .subscribe((top10HashTags: HashTag[]) => {
          this.top10HashTags = top10HashTags;
        });

      this._twitterStreamDataService.getTotalNumberOfTweets()
        .subscribe((totalTweets: number) => {
          this.totalTweets = totalTweets;
        });
    }, 15000);
  }

  ngOnDestroy() {
    if (this.interval) {
      clearInterval(this.interval);
    }
  }
}
