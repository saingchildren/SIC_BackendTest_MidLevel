using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MercuryTest.SwaggerFilters
{
    public class ACPDExampleFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if ((context.ApiDescription.HttpMethod == "GET" || context.ApiDescription.HttpMethod == "DELETE") && operation.Parameters != null)
            {
                var sidParam = operation.Parameters.FirstOrDefault(p => p.Name == "sid");
                if (sidParam != null)
                {
                    sidParam.Example = new OpenApiString("0Q108101034114848519");
                }
            }

            if (context.ApiDescription.HttpMethod == "PUT")
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["application/json"] = new OpenApiMediaType
                        {
                            Schema = context.SchemaGenerator.GenerateSchema(context.MethodInfo.GetParameters()[1].ParameterType, context.SchemaRepository),
                            Example = new OpenApiObject
                            {
                                ["acpD_Cname"] = new OpenApiString("王亦瀧"),
                                ["acpD_Ename"] = new OpenApiString("SIC_TEST"),
                                ["acpD_Sname"] = new OpenApiString("SIC"),
                                ["acpD_Email"] = new OpenApiString("sic900626@gmail.com"),
                                ["acpD_Status"] = new OpenApiInteger(1),
                                ["acpD_Stop"] = new OpenApiBoolean(false),
                                ["acpD_StopMemo"] = new OpenApiString(""),
                                ["acpD_LoginID"] = new OpenApiString("sic"),
                                ["acpD_LoginPWD"] = new OpenApiString("sic"),
                                ["acpD_Memo"] = new OpenApiString("這是透過測試資料產生的帳號"),
                                ["acpD_NowID"] = new OpenApiString("ADMIN")
                            }
                        }
                    }
                };
            }
        }
    }
}
