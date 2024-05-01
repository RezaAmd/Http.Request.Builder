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

Console.WriteLine($"{response.StatusCode} {response.Content}");

public class InputData
{
    public string Name { get; set; }
    public string Surname { get; set; }
}