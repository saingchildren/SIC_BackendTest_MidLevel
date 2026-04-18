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

        public async Task<string> READ(string sid)
        {
            var data = await _appDbContext
                .MyOffice_ACPD.AsNoTracking()
                .FirstOrDefaultAsync(x => x.ACPD_SID == sid);

            if (data == null)
            {
                return "{}";
            }

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

        public async Task<bool> Update(string sid, MyOffice_ACPD acpd)
        {
            var existingData = await _appDbContext.MyOffice_ACPD.FindAsync(sid);

            if (existingData == null)
                return false;

            acpd.ACPD_SID = sid; // 防止SID被改變
            acpd.ACPD_UPDDateTime = DateTime.Now;

            _appDbContext.Entry(existingData).CurrentValues.SetValues(acpd);

            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> Delete(string sid)
        {
            var existingData = await _appDbContext.MyOffice_ACPD.FindAsync(sid);

            if (existingData == null)
                return false;

            _appDbContext.MyOffice_ACPD.Remove(existingData);

            return await _appDbContext.SaveChangesAsync() > 0;
        }
    }
}
