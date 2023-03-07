using IdentityModel.Client;

namespace Gateway.DelegateHandlers
{
    public class TokenExchangeDelgateHandler:DelegatingHandler
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string _accesToken;

        public TokenExchangeDelgateHandler(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
 
        }

        public async Task<string> GetToken(string requestToken)
        {
            if (!string.IsNullOrEmpty(_accesToken))
            {
                return _accesToken;
            }

            var discoveryDocument = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest { Address = _configuration["IdentityServerURL"] });
            if (discoveryDocument.IsError)
            {
                throw discoveryDocument.Exception;
            }

            TokenExchangeTokenRequest tokenExchangeTokenRequest = new TokenExchangeTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = _configuration["ClientId"],
                ClientSecret = _configuration["ClientSecret"],
                GrantType = _configuration["TokenGrantType"],
                SubjectToken = requestToken,
                SubjectTokenType = _configuration["SubjectTokenType"],
                Scope= "openid discount_fullpermission payment_fullpermission"
            };
            var tokenResponse =await _httpClient.RequestTokenExchangeTokenAsync(tokenExchangeTokenRequest);
            if (tokenResponse.IsError)
            {
                throw  tokenResponse.Exception;
            }
            _accesToken = tokenResponse.AccessToken;
            return _accesToken;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestToken = request.Headers.Authorization.Parameter;
            var newToken = await GetToken(requestToken);
            request.SetBearerToken(newToken);
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
