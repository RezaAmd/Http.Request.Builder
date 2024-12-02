<div align="center">
  <p>
    <a href="https://www.nuget.org/packages/Http.Request.Builder/" target="_blank">
      <img src="https://github.com/user-attachments/assets/3bbf5f0e-112d-4f54-8782-222618c17cb3" width="100px" />
    </a>
  <h1>Http.Request.Builder</h1>
  </p>
  <p>
    <a href="https://www.nuget.org/packages/Http.Request.Builder/" target="_blank"><img src="https://img.shields.io/nuget/v/Http.Request.Builder.svg" alt="NuGet" /></a>
    <a href="https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/overview" target="_blank"><img src="https://badgen.net/badge/.net/v8.0/purple"/></a>
    <a href="https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/overview" target="_blank"><img src="https://badgen.net/badge/.NetStandard/v2.1/blue"/></a>
    <a href="https://www.nuget.org/packages/Http.Request.Builder" target="_blank"><img src="https://img.shields.io/nuget/dt/Http.Request.Builder"/></a>
  </p>
  <p>An .net package for easier sending request as builder use retry pattern.</p>
</div>

Http request as builder pattern for easier to use.
You can config it to try request again a few more times, if the request was failed. You can map the response in an object of class.

## How to install?
In .Net - [NuGet](https://www.nuget.org/packages/Http.Request.Builder):
```
Install-Package Http.Request.Builder
```

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
    .WithBearerToken("JWT_TOKEN")                 // <--- Authentication as bearer token here!
    .WithHeader("Accept", "application/json")     // <--- Your custom headers!
    .WithHeader("CUSTOM_KEY", "CUSTOM_VALUE")     // <--- Your custom headers!
    .WithRetryAttemptsForFailed(options =>
    {
        options.MaxRetries = 5;                   //   <--- Number of retries after failure!
        options.Delay = TimeSpan.FromSeconds(1);  //   <--- Number of retries after failure!
    })
    .Build();
```

## Asp.Net core example:
☝ Create your custom client (for example we create `WeatherClient.cs`) and register in `startup.cs` or `program.cs`.
### `Program.cs`:
```csharp
// ...
builder.Services.AddHttpClient<WeatherClient>();
// ...
```
### `WeatherClient.cs`
```csharp
    public class WeatherClient
    {
        private readonly HttpClient _httpClient;

        public WeatherClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task FetchAsync()
        {
            var request = RequestBuilder.Create("https://Weather_URL/Get", HttpMethod.Get, _httpClient) // <--- Pass your client here!
              .Build();

            var response = await request.SendAsync();
        }
    }
```
