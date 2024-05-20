# Http Request Builder
Http request as builder pattern for easier to use.
You can config it to try request again a few more times, if the request was failed. You can map the response in an object of class.

## How to install?
In .Net - [NuGet](https://www.nuget.org/packages/Http.Request.Builder):
```
Install-Package Http.Request.Builder
```

Quick use:
```
// Prepare request object.
var request = RequestBuilder.Create("YOUR_URL", HttpMethod.GET)
    .Build();
    
// Send request and get response.
var response = await request.SendAsync();

//Console.WriteLine($"{response.StatusCode} {response.Content}");
```
