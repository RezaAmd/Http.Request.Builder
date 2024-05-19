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
