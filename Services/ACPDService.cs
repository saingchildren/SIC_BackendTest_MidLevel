using System.Text.Json;
using MercuryTest.Data;
using MercuryTest.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MercuryTest.Services
{
    public class ACPDService : IACPDService
    {
        private readonly AppDbContext _appDbContext;

        public ACPDService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<string> READ()
        {
            var data = await _appDbContext.MyOffice_ACPD.ToListAsync();
            string json = JsonSerializer.Serialize(data);
            return json;
        }
    }
}
