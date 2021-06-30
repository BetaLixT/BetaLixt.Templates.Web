// <copyright file="AuthenticationServiceCollectionExtensions.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace BetaLixT.Templates.Web.Standard.Api.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BetaLixT.Templates.Web.Standard.Api.Authentication.ApiKeyAuthentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Extension class for registering auth services in DI container.
    /// </summary>
    public static class AuthenticationServiceCollectionExtensions
    {
        // - JWT configuration keys
        private static readonly string JwtClientIdConfigurationSettingsKey = "ClientId";
        private static readonly string JwtApplicationIdURIConfigurationSettingsKey = "ApplicationIdURI";
        private static readonly string JwtValidIssuersConfigurationSettingsKey = "ValidIssuers";
        private static readonly string JwtAuthorityConfigurationKey = "Authority";

        // - API key configuration keys
        private static readonly string ApiKeySecretConfigurationSettingsKey = "ApiKey";
        private static readonly string ApiKeyHeaderConfigurationSettingsKey = "ApiKeyHeaderName";


        /// <summary>
        /// Registers jwt authentication service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="schemeName">Name of the auth policy and scheme</param>
        public static void RegisterJwtAuthenticationService(
            this IServiceCollection services,
            IConfiguration configuration,
            string schemeName)
        {
            AuthenticationServiceCollectionExtensions.ValidateAuthenticationConfigurationSettings(configuration);

            // - Adding JWT authentication scheme
            services.AddAuthentication()
                .AddJwtBearer(schemeName, options =>
                {
                    options.Authority = configuration[JwtAuthorityConfigurationKey];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudiences = AuthenticationServiceCollectionExtensions.GetValidAudiences(configuration),
                        ValidIssuers = AuthenticationServiceCollectionExtensions.GetValidIssuers(configuration),
                        ValidateAudience = true,
                        ValidateIssuer = true,
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnForbidden = context =>
                        {
                            return Task.CompletedTask;
                        }
                    };
                });

            // - Adding authorization policy using the created scheme
            services.AddAuthorization(options =>
            {
                options.AddPolicy(schemeName, new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(schemeName)
                    .Build());
            });
            
                
        }

        /// <summary>
        /// Registers api key authentication service
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="schemeName"></param>
        public static void RegsiterApiKeyAuthenticationService(
            this IServiceCollection services,
            IConfiguration configuration,
            string schemeName)
        {
            // - Adding API key authentication scheme
            services.AddAuthentication()
                .AddApiKeySupport(schemeName, options =>
                {
                    options.ApiKey = configuration[ApiKeySecretConfigurationSettingsKey];
                    options.ApiKeyHeaderName = configuration[ApiKeyHeaderConfigurationSettingsKey];
                });

            // - Adding authorization policy using the ccreated scheme
            services.AddAuthorization(options =>
            {
                options.AddPolicy(schemeName, new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes(schemeName)
                    .Build());
            });

        }

        public static void RegisterAuthorizationPolicy<Requirement, Handler>(
            this IServiceCollection services,
            string policyName,
            Requirement policyRequirement)
            where Requirement : IAuthorizationRequirement where Handler : class, IAuthorizationHandler
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy(
                    policyName,
                    policyBuilder => policyBuilder.AddRequirements(policyRequirement));
            });

            services.AddSingleton<IAuthorizationHandler, Handler>();
        }

        private static void ValidateAuthenticationConfigurationSettings(IConfiguration configuration)
        {
            var clientId = configuration[AuthenticationServiceCollectionExtensions.JwtClientIdConfigurationSettingsKey];
            if (string.IsNullOrWhiteSpace(clientId))
            {
                throw new ApplicationException("AzureAD ClientId is missing in the configuration file.");
            }

            var applicationIdURI = configuration[AuthenticationServiceCollectionExtensions.JwtApplicationIdURIConfigurationSettingsKey];
            if (string.IsNullOrWhiteSpace(applicationIdURI))
            {
                throw new ApplicationException("AzureAD ApplicationIdURI is missing in the configuration file.");
            }

            var validIssuers = configuration[AuthenticationServiceCollectionExtensions.JwtValidIssuersConfigurationSettingsKey];
            if (string.IsNullOrWhiteSpace(validIssuers))
            {
                throw new ApplicationException("AzureAD ValidIssuers is missing in the configuration file.");
            }
        }

        private static IEnumerable<string> GetSettings(IConfiguration configuration, string configurationSettingsKey)
        {
            var configurationSettingsValue = configuration[configurationSettingsKey];
            var settings = configurationSettingsValue
                ?.Split(new char[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries)
                ?.Select(p => p.Trim());
            if (settings == null)
            {
                throw new ApplicationException($"{configurationSettingsKey} does not contain a valid value in the configuration file.");
            }

            return settings;
        }

        private static IEnumerable<string> GetValidAudiences(IConfiguration configuration)
        {
            var clientId = configuration[AuthenticationServiceCollectionExtensions.JwtClientIdConfigurationSettingsKey];

            var applicationIdURI = configuration[AuthenticationServiceCollectionExtensions.JwtApplicationIdURIConfigurationSettingsKey];

            var validAudiences = new List<string> { clientId, applicationIdURI.ToLower() };

            return validAudiences;
        }

        private static IEnumerable<string> GetValidIssuers(IConfiguration configuration)
        {

            var validIssuers =
                AuthenticationServiceCollectionExtensions.GetSettings(
                    configuration,
                    AuthenticationServiceCollectionExtensions.JwtValidIssuersConfigurationSettingsKey);

            return validIssuers;
        }

        private static bool AudienceValidator(
            IEnumerable<string> tokenAudiences,
            SecurityToken securityToken,
            TokenValidationParameters validationParameters)
        {
            if (tokenAudiences == null || tokenAudiences.Count() == 0)
            {
                throw new ApplicationException("No audience defined in token!");
            }

            var validAudiences = validationParameters.ValidAudiences;
            if (validAudiences == null || validAudiences.Count() == 0)
            {
                throw new ApplicationException("No valid audiences defined in validationParameters!");
            }

            foreach (var tokenAudience in tokenAudiences)
            {
                if (validAudiences.Any(validAudience => validAudience.Equals(tokenAudience, StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
            }

            return true;
        }


    }
}