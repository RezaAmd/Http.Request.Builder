using HttpRequestBuilder.Builder;

InputData inputData = new()
{
    Name = "Mohammad",
    Surname = "Karimi"
};

var request = RequestBuilder.Create("https://google.com")
    .WithBearerToken("JWT_BEARER_TOKEN")
    .WithDataFromBodyAsJson(inputData)
    .WithHeader("x-api-key", "This is an extra header")
    .WithHeader("KEY", "VALUE")
    .Build();

var response = await request.SendAsync();


async Task a()
{
    var client = new HttpClient();
    var request = new HttpRequestMessage(HttpMethod.Get, "https://google.com");

    var collection = new List<KeyValuePair<string, string>>();
    collection.Add(new("ttt", "ooooo"));
    var content = new FormUrlEncodedContent(collection);
    request.Content = content;

    var response = await client.SendAsync(request);
    response.EnsureSuccessStatusCode();
    Console.WriteLine(await response.Content.ReadAsStringAsync());
}



Console.WriteLine($"{response.StatusCode} {response.Content}");

public class InputData
{
    public string Name { get; set; }
    public string Surname { get; set; }
}