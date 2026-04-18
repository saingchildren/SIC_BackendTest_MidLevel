using System.Data;
using System.Text.Json;
using Dapper;
using MercuryTest.Data;
using MercuryTest.Interfaces;
using MercuryTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MercuryTest.Services
{
    public class ACPDService : IACPDService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDbConnection _dbContext;

        public ACPDService(AppDbContext appDbContext, IDbConnection dbContext)
        {
            _appDbContext = appDbContext;
            _dbContext = dbContext;
        }

        public async Task<string> READ()
        {
            var data = await _appDbContext.MyOffice_ACPD.ToListAsync();
            string json = JsonSerializer.Serialize(data);
            return json;
        }

        public async Task<bool> Create(MyOffice_ACPD acpd)
        {
            var param = new DynamicParameters();
            var parameters = new DynamicParameters();
            parameters.Add("@TableName", "MyOffice_ACPD", DbType.String, ParameterDirection.Input);

            parameters.Add(
                "@ReturnSID",
                dbType: DbType.String,
                direction: ParameterDirection.Output,
                size: 20
            );

            await _dbContext.ExecuteAsync(
                "NEWSID",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            string newSid = parameters.Get<string>("@ReturnSID");
            DateTime now = DateTime.Now;

            if (string.IsNullOrEmpty(newSid))
                return false;

            acpd.ACPD_SID = newSid;
            acpd.ACPD_NowDateTime = now;
            acpd.ACPD_UPDDateTime = now;
            await _appDbContext.MyOffice_ACPD.AddAsync(acpd);
            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}
