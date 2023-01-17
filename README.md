# Twitter Stream Project

## Architecture
This is the overall architecture of the project.

![Architecture diagram](/images/architecture_diagram.png "Architecture diagram")

Note that I've used the approach in this [article](https://learn.microsoft.com/en-us/azure/stream-analytics/stream-analytics-twitter-sentiment-analysis-trends) from Microsoft as a reference architecture with a couple modifications as shown above.

**I haven't used any of the code in the article's github reference, instead I coded my own implementation from scratch using my own modified architecture above.**

### Data Flow
- Listener (worker) service monitors the Twitter sample stream endpoint for incoming tweets.
- Once a tweet is received, information about the actual text of the tweet and hash tags from the tweet are extracted and sent to the event hub. Tweet information is sent to the *tweet-received* topic and hash tag information is sent to the *hash-tag-received* topic.
- Data for both tweets and hash tags from the event hub are piped to an Azure SQL Server database via an Azure Stream Analytics Job as shown in the diagram above. 
- Web API exposes endpoints for returning the top 10 hash tags given an hour window value (for example hourWindow=1 means get the top 10 hash tags for the past hour, etc..) and total number of tweets received.
- Angular app then calls this Web API and displays the needed data in the browser, the app is set to refresh every 30 seconds so updated data can be retrieved and shown to the user.


## Twitter Stream Listener 
### Local Configuration
The Twitter Stream Listener solution file is located in this folder core/TwitterStreamListener

Local configuration uses secret manager, run the following command from the directory where the Listener project file is:
```
dotnet user-secrets set "TwitterApi:ConsumerKey" "<Enter your consumer key here>"
dotnet user-secrets set "TwitterApi:ConsumerSecret" "<Enter your consumer secret here>"
dotnet user-secrets set "TwitterApi:BearerToken" "<Enter your bearer token here>"
dotnet user-secrets set "EventHub:ConnectionString" "<Enter your event hub connection string here>"
```

For security purposes, we store these information to secrets manager so that we don't include them in the source control.

### Notes
Twitter Stream Listener uses [tweetinvi](https://github.com/linvi/tweetinvi) to listen the sample stream API endpoint, and it also takes care of the rate limit handling.

## Event Hub
### Azure Cloud Configuration
Event hub namespace is setup in Azure called `tweet-stream`, and two event hub is added under this namespace, `tweet-received` and `hash-tag-received`

The ARM template is added under the infra/azure/arm folder for reference.

## SQL Server Database
### Azure Cloud Configuration
TwitterStreamDb SQL Server database is setup, and two tables are added under this database, one is `[dbo].[HashTags]` and the other one is `[dbo].[Tweets]`.

A stored procedure is also added to retrieve the top 10 hash tags given an hour window value `[dbo].[SpGetTop10HashTagsByHourWindow] `, for example the top 10 hash tags for the past 5 hours.

The ARM template is added under the infra/azure/arm folder for reference.

## Stream Analytics
### Azure Cloud Configuration
A stream analytics job is setup that pipes data from input to ouput, in this case, input is set to be the event hub, and output is the SQL Server DB. Shown below is the query snapshot for reference.

![Stream analytics query](/images/stream_analytics_query.png "Stream analytics query")

The ARM template is added under the infra/azure/stream-analytics folder for reference.

## Twitter Stream Web API
### Local Configuration
The Twitter Stream Web API solution file is located in this folder core/TwitterStreamWebAPI

Local configuration uses secret manager, run the following command from the directory where the Web API project file is:
```
dotnet user-secrets set "ConnectionStrings:TwitterStreamDb" "<Enter your connection string here>"
```

For security purposes, we store these information to secrets manager so that we don't include them in the source control.

## Twitter Stream Angular App
### UI Screenshot
Here's a screenshot of the UI

![UI screenshot](/images/ui_screenshot.png "UI screenshot")

**The app component is set to do a refresh every 15 seconds to retrieve the most recent data from the Web API.**

### Local Installation
The Twitter Stream Angular App project is located in this folder ui/twitter-stream-ng

Run `npm install` to install the packages and run `ng serve` to run the angular app locally.

Navigate to `http://localhost:4200` to access the app.

## Further Improvements
### `backfill_minutes` parameter
One major improvement for this project is to make use of the `backfill_minutes` query parameter when calling the Twitter sample stream API endpoint, we can keep track of how much time since the last failed attempt, persist that somewhere and when the app is ready to listen again, we passed that as the `backfill_minutes` to retrieve the lost tweets we missed. This parameter is in minutes so for example `backfill_minutes=5` makes the stream listener start 5 minutes from when the call is made, this means that duplicate delivery of tweets might happen and de-duplication strategies must be implemented on the consuming side to ensure data integrity.

This implementation was not included in the code because using `backfill_minutes` requires an approved [Academic Research](https://developer.twitter.com/en/products/twitter-api/academic-research) level of access. See [Twitter's Sample Stream API](https://developer.twitter.com/en/docs/twitter-api/tweets/volume-streams/api-reference/get-tweets-sample-stream) for more info.
