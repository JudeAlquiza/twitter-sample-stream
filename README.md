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


## Local Development Setup
### Twitter Stream Listener 
#### Configuration
The Twitter Stream Listener solution file is located in this folder core/TwitterStreamListener

Local configuration uses secret manager, run the following command from the directory where the Listener project file is:
```
dotnet user-secrets set "TwitterApi:ConsumerKey" "<Enter your consumer key here>"
dotnet user-secrets set "TwitterApi:ConsumerSecret" "<Enter your consumer secret here>"
dotnet user-secrets set "TwitterApi:BearerToken" "<Enter your bearer token here>"
dotnet user-secrets set "EventHub:ConnectionString" "<Enter your event hub connection string here>"
```

For security purposes, we store these information to secrets manager so that we don't include them in the source control.

### Twitter Stream Web API
#### Configuration
The Twitter Stream Web API solution file is located in this folder core/TwitterStreamWebAPI

Local configuration uses secret manager, run the following command from the directory where the Web API project file is:
```
dotnet user-secrets set "ConnectionStrings:TwitterStreamDb" "<Enter your connection string here>"
```

For security purposes, we store these information to secrets manager so that we don't include them in the source control.

### Twitter Stream Angular App
#### Installation
The Twitter Stream Angular App project is located in this folder ui/twitter-stream-ng

Run `npm install` to install the packages and run `ng serv`e to run the angular app locally.

Navigate to `https://localhost:4200` to access the app.

## Azure Cloud Setup
