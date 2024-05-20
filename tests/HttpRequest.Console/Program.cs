using Http.Request.Builder.Builder;

InputData inputData = new()
{
    Username = "RezaAmd",
    Password = "123456"
};

var request = RequestBuilder.Create("https://localhost:44383/authentication/signIn", HttpMethod.Post)
    .WithDataFromBodyAsJson(inputData)
    .WithHeader("CUSTOM_HEADER", "YOUR_API_KEY")
    .WithRetryAttemptsForFailed(5)
    .Build();

var response = await request.SendAsync();

Console.WriteLine($"{response.StatusCode} {response.Content}");


public class InputData
{
    public string Username { get; set; }
    public string Password { get; set; }
}