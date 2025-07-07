using SouthHome.Backend.Common.Http;
using System.Text.Json;

namespace SouthHome.Backend.Common
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // 执行下一个中间件
            }
            catch(ArgumentException ex)
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                ServiceResponse<string> response = ServiceResponse<string>.Error(ex.Message, 400);
                await context.Response.WriteAsync(JsonSerializer.Serialize(response, _jsonOptions));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 200;
                context.Response.ContentType = "application/json";
                ServiceResponse<string> response = ServiceResponse<string>.Error(ex.Message, 400);
                await context.Response.WriteAsync(JsonSerializer.Serialize(response, _jsonOptions));
            }
        }
    }
}
