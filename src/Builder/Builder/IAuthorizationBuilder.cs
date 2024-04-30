namespace HttpRequestBuilder.Builder
{
    public interface IAuthorizationBuilder :
            IBearerTokenAuthBuilder,
            IRequestBuilder
    {
    }
}