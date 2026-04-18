using System.Data;
using System.Text.Json;
using Dapper;
using MercuryTest.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace MercuryTest.ActionFilters
{
    public class LogActionFilter : IAsyncActionFilter
    {
        private readonly AppDbContext _context;

        public LogActionFilter(AppDbContext context)
        {
            _context = context;
        }

        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next
        )
        {
            Guid groupId = Guid.NewGuid();

            string apiPath = context.HttpContext.Request.Path;
            string programName = context.ActionDescriptor.DisplayName ?? "Unknown";
            string executionInfo = JsonSerializer.Serialize(context.ActionArguments);
            var executedContext = await next();
            try
            {
                var connection = _context.Database.GetDbConnection();

                var parameters = new DynamicParameters();

                parameters.Add("@_InBox_ReadID", 0, DbType.Byte);
                parameters.Add("@_InBox_SPNAME", apiPath, DbType.String);
                parameters.Add("@_InBox_GroupID", groupId, DbType.Guid);
                parameters.Add("@_InBox_ExProgram", programName, DbType.String);
                parameters.Add("@_InBox_ActionJSON", executionInfo, DbType.String);

                parameters.Add(
                    "@_OutBox_ReturnValues",
                    dbType: DbType.String,
                    direction: ParameterDirection.Output,
                    size: -1
                );

                await connection.ExecuteAsync(
                    "usp_AddLog",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"寫入 Log SP 失敗: {ex.Message}");
            }
        }
    }
}
