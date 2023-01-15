# Twitter Stream Project

## Local Setup
### Twitter Stream Listener 
#### Configuration
The Twitter Stream Listener solution file is located at core/TwitterStreamListener

Local configuration uses secret manager, run the following command from the directory in which the project file exists:
```
dotnet user-secrets set "TwitterApi:ConsumerKey" "<Enter your consumer key here>"
dotnet user-secrets set "TwitterApi:ConsumerSecret" "<Enter your consumer secret here>"
dotnet user-secrets set "TwitterApi:BearerToken" "<Enter your bearer token here>"
```

For security purposes, we store these information to secrets manager so that we don't include them in the source control.
