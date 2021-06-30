using Microsoft.Extensions.Options;
using System;

namespace BetaLixT.Templates.Web.Standard.Api.Authentication.ApiKeyAuthentication
{
    public class ApiKeyAuthenticationPostConfigureOptions : IPostConfigureOptions<ApiKeyAuthenticationOptions>
    {
        public void PostConfigure(string name, ApiKeyAuthenticationOptions options)
        {
            if (string.IsNullOrEmpty(options.ApiKeyHeaderName) || string.IsNullOrEmpty(options.ApiKey))
            {
                throw new Exception("Invalid configuration");
            }

        }
    }
}
