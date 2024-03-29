﻿using BetaLixT.Templates.Web.Standard.Api.Models.Responses;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
namespace BetaLixT.Templates.Web.Standard.Api.Formatters
{
    public class JsonFormatter : TextOutputFormatter
    {
        public JsonFormatter()
        {
            // Add the supported media type.
            SupportedMediaTypes.Add("application/json");
            SupportedEncodings.Add(Encoding.UTF8);  
        }

        protected override bool CanWriteType(Type? type)
        {
           return true; 
        }

        public override async Task WriteResponseBodyAsync(
            OutputFormatterWriteContext ctx,
            Encoding selectedEncoding)
        {
            var sw = new StreamWriter(ctx.HttpContext.Response.Body);
            
            var resp = ctx.Object as SuccessResponseContent;
            JsonTextWriter writer = new JsonTextWriter(sw);
            // await sw.WriteAsync("tewstsets");
            writer = await resp.ToJsonAsync(writer);
            await sw.FlushAsync();
        }
    }
}
