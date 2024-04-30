using HttpRequestBuilder.Request;

var request = RequestBuilder.Create()
    .WithUrl("")
    .WithBearerToken("JWT_BEARER_TOKEN")
    .WithHeader("x-api-key", "This is an extra header")
    .WithHeader("", "")
    .Build();

var response = await request.SendAsync();

Console.WriteLine($"{response.StatusCode} {response.Content}");