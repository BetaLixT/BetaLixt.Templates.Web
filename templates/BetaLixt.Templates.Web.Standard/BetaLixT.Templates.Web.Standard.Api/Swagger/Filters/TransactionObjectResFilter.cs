using BetaLixT.Templates.Web.Standard.Api.Models.Responses;
using BetaLixT.Templates.Web.Standard.Api.Swagger.Attributes;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BetaLixT.Templates.Web.Standard.Api.Swagger.OperationFilters
{
    public class TransactionObjectResFilter : IOperationFilter
    {
        public void Apply(
            OpenApiOperation operation,
            OperationFilterContext context)
        {
            if (operation.Responses.Count > 0)
            {
                foreach(var response in operation.Responses.Where(x => x.Key.StartsWith('2')))
                {
                    if (response.Value.Content.Count > 0)
                    {
                        foreach (var content in response.Value.Content)
                        {
                            var schema = content.Value.Schema;

                            if (schema == null)
                            {
                                content.Value.Schema = new OpenApiSchema
                                {
                                    Type = "object",
                                    Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    { "statusMessage", new OpenApiSchema { Type = "string", Nullable = false, Example = new OpenApiString(ResponseContentStatusMessages.Success) } }
                                }
                                };
                            }
                            else if (content.Value.Schema.Type == "array")
                            {
                                content.Value.Schema = new OpenApiSchema
                                {
                                    Type = "object",
                                    Properties = new Dictionary<string, OpenApiSchema>
                                {
                                    { "statusMessage", new OpenApiSchema { Type = "string", Nullable = false, Example = new OpenApiString(ResponseContentStatusMessages.Success) } },
                                    { "resultData", schema },
                                    { "totalCount", new OpenApiSchema { Type = "integer", Nullable = false } },
                                }
                                };
                            }
                            else
                            {
                                content.Value.Schema = new OpenApiSchema
                                {
                                    Type = "object",
                                    Properties = new Dictionary<string, OpenApiSchema>
                                    {
                                        { "statusMessage", new OpenApiSchema { Type = "string", Nullable = false, Example = new OpenApiString(ResponseContentStatusMessages.Success) } },
                                        { "resultData", schema },
                                    }
                                };
                            }
                        }
                    }
                    else
                    {
                        response.Value.Content.Add("application/json", new OpenApiMediaType {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = new Dictionary<string, OpenApiSchema>
                                    {
                                        { "statusMessage", new OpenApiSchema { Type = "string", Nullable = false, Example = new OpenApiString(ResponseContentStatusMessages.Success) } },
                                    }
                            },
                            Encoding = new Dictionary<string, OpenApiEncoding>
                            {
                                /*{ "UTF-8", new OpenApiEncoding}*/
                            }
                        });
                    }
                }
            }
            // if (operation.Parameters == null)
            //     operation.Parameters = new List<OpenApiParameter>();

            //var temp = operation.Responses;
        	// operation.Responses.Add("",
			// new OpenApiResponse{
			// 	Content = new Dictionary<string, OpenApiMediaType> {
			// 		{"test", new OpenApiMediaType{}}
			// 	}	
			// }
		    //);
            // if (!context.ApiDescription.TryGetMethodInfo(out var methodInfo))
            // {
            //     return;
            // }

            // // TODO Find a different way to do this instead of relying on reflections
            // var respondsWiths = methodInfo?.DeclaringType?.CustomAttributes.OfType<RespondsWith>();
            // if (respondsWiths != null)
            // {
            //     foreach (var respondsWith in respondsWiths)
            //     {
            //         operation.Responses.Add(
            //             respondsWith.StatusCode.ToString(),
            //             new OpenApiResponse { Description = "Unauthorized" });
            //         new OpenApiResponse { Content = new Dictionary<string, OpenApiMediaType>() {
            //             { "", new OpenApiMediaType{ 
            //             }}
            //         }};
            //     }

            //     if (!operation.Responses.Any(r => r.Key == StatusCodes.Status401Unauthorized.ToString()))
            //     {
            //     }
            // }

        }
    }
}
