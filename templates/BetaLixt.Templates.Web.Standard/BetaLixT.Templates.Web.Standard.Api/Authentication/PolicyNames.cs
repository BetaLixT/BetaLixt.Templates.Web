// <copyright file="PolicyNames.cs" company="Microsoft">
// Copyright (c) Microsoft. All rights reserved.
// </copyright>

namespace BetaLixT.Templates.Web.Standard.Api.Authentication
{
    /// <summary>
    /// This class lists the names of the custom authorization policies in the project.
    /// </summary>
    public class PolicyNames
    {
        public const string AzureAuth = "Azure";
        /// <summary>
        /// The name of the issuer validation policy
        /// </summary>
        public const string JobApiKey = "JobApiKey";

        /// <summary>
        /// The name of the validate users policy
        /// </summary>
        public const string ValidateAdminPolicy = "ValidateAdminPolicy";
    }
}
