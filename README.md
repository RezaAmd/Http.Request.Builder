<div align="center">
  <p>
    <a href="https://www.nuget.org/packages/Http.Request.Builder/" target="_blank">
      <img src="#" width="100px" />
    </a>
  <h1>Http Request Builder</h1>
  </p>
  <p>
    <a href="https://www.nuget.org/packages/Http.Request.Builder/" target="_blank"><img src="https://img.shields.io/nuget/v/Http.Request.Builder.svg" alt="NuGet" /></a>
    <a href="https://docs.microsoft.com/en-us/aspnet/core/release-notes/aspnetcore-5.0" target="_blank"><img src="https://badgen.net/badge/.net/v8.0/purple"/></a>
    <a href="https://www.nuget.org/packages/Http.Request.Builder" target="_blank"><img src="https://img.shields.io/nuget/dt/Http.Request.Builder"/></a>
  </p>
  <p>Dotnet service wrapper for <a href="https://github.com/restsharp/RestSharp"> RestSharp </a> package for REST request.</p>
</div>

# Http Request Builder
Http request as builder pattern for easier to use.
You can send request and deserialize response of request.

Example to use:
```
// Prepare request object.
var request = RequestBuilder.Create("YOUR_URL", HttpMethod.GET)
    .WithHeader("CUSTOM_HEADER", "YOUR_API_KEY")
    .Build();
    
// Send request and get response.
var response = await request.SendAsync();

//Console.WriteLine($"{response.StatusCode} {response.Content}");
```
