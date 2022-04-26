using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using System;
using System.Linq;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Comparers;
using Microsoft.OpenApi.Models;
using Microsoft.Azure.Functions.Worker.Core;
using Microsoft.Azure.Functions.Worker.Extensions;




namespace emrsn.com.fun.datalake
{
    /// <summary>
    /// This represents the options entity for OpenAPI metadata configuration.
    /// </summary>
    public class DefaultOpenApiConfigurationOptions : IOpenApiConfigurationOptions
    {
        private const string OpenApiDocVersionKey = "OpenApi__DocVersion";
        private const string OpenApiDocTitleKey = "OpenApi__DocTitle";
        private const string OpenApiHostNamesKey = "OpenApi__HostNames";
        private const string OpenApiVersionKey = "OpenApi__Version";
        private const string FunctionsRuntimeEnvironmentKey = "AZURE_FUNCTIONS_ENVIRONMENT";
        private const string ForceHttpKey = "OpenApi__ForceHttp";
        private const string ForceHttpsKey = "OpenApi__ForceHttps";

        /// <inheritdoc />
        public virtual OpenApiInfo Info { get; set; } = new OpenApiInfo()
        {
            Version = GetOpenApiDocVersion(),
            Title = GetOpenApiDocTitle(),
            Description= $""
        };

        /// <inheritdoc />
        public virtual List<OpenApiServer> Servers { get; set; } = new List<OpenApiServer>()
    {
        new OpenApiServer() { Url = "https://fa-z-autosol-pmogit-n-001.azurewebsites.net/api/asl-ms-ordrhstry-datalake-betsy/getOrderHistoryBetsy" , Description= "Development server" },
        new OpenApiServer() { Url = " http://fa-z-autosol-microservice-n-001.azurewebsites.net/api/asl-ms-ordrhstry-datalake-betsy/getOrderHistoryBetsy" , Description= "Stage server" },
         new OpenApiServer() { Url = " http://fa-z-autosol-microservice-p-001.azurewebsites.net/api/asl-ms-ordrhstry-datalake-betsy/getOrderHistoryBetsy" , Description= "Production server" },
    };
        public virtual List<OpenApiTag> OpenApiTags { get; set; } = GetTagValues();

        /// <inheritdoc />
        public virtual OpenApiVersionType OpenApiVersion { get; set; } = GetOpenApiVersion();

        /// <inheritdoc />
        public virtual bool IncludeRequestingHostName { get; set; } = IsFunctionsRuntimeEnvironmentDevelopment();

        /// <inheritdoc />
        public virtual bool ForceHttp { get; set; } = IsHttpForced();

        /// <inheritdoc />
        public virtual bool ForceHttps { get; set; } = IsHttpsForced();

        /// <inheritdoc />
        public virtual List<IDocumentFilter> DocumentFilters { get; set; } = new List<IDocumentFilter>();

        /// <summary>
        /// Gets the OpenAPI document version.
        /// </summary>
        /// <returns>Returns the OpenAPI document version.</returns>
        protected static string GetOpenApiDocVersion()
        {
            var version = Environment.GetEnvironmentVariable(OpenApiDocVersionKey) ?? DefaultOpenApiDocVersion();

            return version;
        }

        /// <summary>
        /// Gets the OpenAPI document title.
        /// </summary>
        /// <returns>Returns the OpenAPI document title.</returns>
        protected static string GetOpenApiDocTitle()
        {
            var title = Environment.GetEnvironmentVariable(OpenApiDocTitleKey) ?? DefaultOpenApiDocTitle();

            return title;
        }

        /// <summary>
        /// Gets the list of hostnames.
        /// </summary>
        /// <returns>Returns the list of hostnames.</returns>
        protected static List<OpenApiServer> GetHostNames()
        {
            var servers = new List<OpenApiServer>();
            var collection = "https://development.gigantic-server.com/v1,https://development.gigantic-server.com/v1";
            if (collection.ToString() =="")
            {
                return servers;
            }

            var hostnames = collection.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                      .Where(h => !string.IsNullOrWhiteSpace(h))
                                      .Select(p => new OpenApiServer() { Url = p.Trim() });

            servers.AddRange(hostnames);

            return servers;
        }

        protected static List<OpenApiTag> GetTagValues()
        {
            var tags = new List<OpenApiTag>();
            var collection = "https://development.gigantic-server.com/v1,https://development.gigantic-server.com/v1";
            if (collection.ToString() =="")
            {
                return tags;
            }

            var hostnames = collection.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                                      .Where(h => !string.IsNullOrWhiteSpace(h))
                                      .Select(p => new OpenApiTag() { Name = p.Trim() });

            tags.AddRange(hostnames);

            return tags;
        }

        /// <summary>
        /// Gets the OpenAPI version.
        /// </summary>
        /// <returns>Returns the OpenAPI version.</returns>
        protected static OpenApiVersionType GetOpenApiVersion()
        {
            var version = Enum.TryParse<OpenApiVersionType>(
                              Environment.GetEnvironmentVariable(OpenApiVersionKey), ignoreCase: true, out var result)
                            ? result
                            : DefaultOpenApiVersion();

            return version;
        }

        /// <summary>
        /// Checks whether the current Azure Functions runtime environment is "Development" or not.
        /// </summary>
        /// <returns>Returns <c>True</c>, if the current Azure Functions runtime environment is "Development"; otherwise returns <c>False</c></returns>
        protected static bool IsFunctionsRuntimeEnvironmentDevelopment()
        {
            var development = Environment.GetEnvironmentVariable(FunctionsRuntimeEnvironmentKey) == "Development";

            return development;
        }

        /// <summary>
        /// Checks whether HTTP is forced or not.
        /// </summary>
        /// <returns>Returns <c>True</c>, if HTTP is forced; otherwise returns <c>False</c>.</returns>
        protected static bool IsHttpForced()
        {
            var development = bool.TryParse(Environment.GetEnvironmentVariable(ForceHttpKey), out var result) ? result : false;

            return development;
        }

        /// <summary>
        /// Checks whether HTTPS is forced or not.
        /// </summary>
        /// <returns>Returns <c>True</c>, if HTTPS is forced; otherwise returns <c>False</c>.</returns>
        protected static bool IsHttpsForced()
        {
            var development = bool.TryParse(Environment.GetEnvironmentVariable(ForceHttpsKey), out var result) ? result : false;

            return development;
        }

        private static OpenApiVersionType DefaultOpenApiVersion()
        {
            return OpenApiVersionType.V3;
        }

        private static string DefaultOpenApiDocVersion()
        {
            return "1.0.0";
        }

        private static string DefaultOpenApiDocTitle()
        {
            return "OpenAPI Document on Auto Sol Microservice design.";
        }
    }
}