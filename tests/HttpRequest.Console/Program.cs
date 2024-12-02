using Http.Request.Builder.Builder;

UserInputData user = new()
{
    Username = "RezaAmd",
    Password = "123456"
};

var request = RequestBuilder.Create("https://localhost:44383/authentication/signIn", HttpMethod.Post)
    .WithDataFromBodyAsJson(user)
    .WithHeader("CUSTOM_HEADER", "YOUR_API_KEY")
    .WithRetryAttemptsForFailed()
    .Build();

var response = await request.SendAsync();

Console.WriteLine($"{response.StatusCode} {response.Content}");


public class UserInputData
{
    public string Username { get; set; }
    public string Password { get; set; }
}