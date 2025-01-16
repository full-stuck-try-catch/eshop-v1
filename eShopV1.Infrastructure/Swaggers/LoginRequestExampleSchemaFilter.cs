using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace eShopV1.Infrastructure.Swaggers
{
    public class LoginRequestExampleSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type.Name == "LoginRequest")
            {
                schema.Example = new OpenApiObject
                {
                    ["email"] = new OpenApiString("hoangdt@gmail.com"),
                    ["password"] = new OpenApiString("123456")
                };
            }
        }
    }
}
