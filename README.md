# Twitter Stream Project

## Local Setup
### Twitter Stream Listener 
#### Configuration
The Twitter Stream Listener solution file is located at core/TwitterStreamListener

Local configuration uses secret manager, run the following command from the directory where the Listener project file is:
```
dotnet user-secrets set "TwitterApi:ConsumerKey" "<Enter your consumer key here>"
dotnet user-secrets set "TwitterApi:ConsumerSecret" "<Enter your consumer secret here>"
dotnet user-secrets set "TwitterApi:BearerToken" "<Enter your bearer token here>"
dotnet user-secrets set "EventHub:ConnectionString" "<Enter your event hub connection string here>"
```

For security purposes, we store these information to secrets manager so that we don't include them in the source control.

#### Comsos Db Query
For reference, this is the query to get the distinct hash tags together with their count.
```sql
SELECT 
  hashTag, 
  COUNT(1) AS hashTagCount 
FROM c JOIN hashTag in c.hashTags 
GROUP BY hashTag
```
