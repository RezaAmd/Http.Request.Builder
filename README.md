<div align="center">
  <p>
    <a href="https://www.nuget.org/packages/Http.Request.Builder/" target="_blank">
      <img src="#" width="100px" />
    </a>
  <h1>Http.Request.Builder</h1>
  </p>
  <p>
    <a href="https://www.nuget.org/packages/Http.Request.Builder/" target="_blank"><img src="https://img.shields.io/nuget/v/Http.Request.Builder.svg" alt="NuGet" /></a>
    <a href="https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-8/overview" target="_blank"><img src="https://badgen.net/badge/.net/v8.0/purple"/></a>
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

Quick use:
```
// Prepare request object.
var request = RequestBuilder.Create("YOUR_URL", HttpMethod.GET)
    .Build();
    
// Send request and get response.
var response = await request.SendAsync();

//Console.WriteLine($"{response.StatusCode} {response.Content}");
```
