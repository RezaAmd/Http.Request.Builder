# Http Request Builder
Http request as builder pattern for easier to use.
You can config it to try request again a few more times, if the request was failed. You can map the response in an object of class.

## Quick use:
```csharp
// Prepare request object.
var request = RequestBuilder.Create("YOUR_URL")
    .Build();
    
// Send request and get response.
var response = await request.SendAsync();

//Console.WriteLine($"{response.StatusCode} {response.Content}");
```

## Configs
```csharp
var request = RequestBuilder.Create("YOUR_URL")
    .WithBearerToken("JWT_TOKEN")             // <--- Authentication as bearer token here!
    .WithHeader("Accept", "application/json") // <--- Your custom headers!
    .WithHeader("CUSTOM_KEY", "CUSTOM_VALUE") // <--- Your custom headers!
    .WithRetryAttemptsForFailed(options =>
    {
        options.MaxRetries = 5;                   //   <--- Number of retries after failure!
        options.Delay = TimeSpan.FromSeconds(1);  //   <--- Number of retries after failure!
    })
    .Build();
```

### Support external HttpClient
```csharp
// Create your client!
var httpClient = new HttpClient();

// Pass to builder!
var request = RequestBuilder.Create("YOUR_URL", HttpMethod.Get, httpClient)
    .Build();
```