using Http.Request.Builder.Builder;

namespace WebApi.Clients
{
    public class TaxBridgeClient
    {
        private readonly ILogger<TaxBridgeClient> _logger;
        private readonly HttpClient _httpClient;
        public TaxBridgeClient(ILogger<TaxBridgeClient> logger,
            HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task GetTokenAsync(CancellationToken cancellationToken)
        {
            var signInModel = new SignInModel()
            {
                username = "admin",
                password = "admin"
            };

            var request = RequestBuilder.Create("https://sandboxrc-dev.rimatsp.com/auth/login2", HttpMethod.Post, _httpClient)
                .WithDataFromBodyAsJson(signInModel)
                .WithRetryAttemptsForFailed()
                .Build();

            var response = await request.SendAsync(cancellationToken);

            _logger.LogInformation(response.StatusCode + " - " + response.Content);
        }
    }

    public class SignInModel
    {
        public string username { get; set; }
        public string password { get; set; }
    }
}
