using MercuryTest.Models;

namespace MercuryTest.Interfaces
{
    public interface IACPDService
    {
        Task<bool> Create(MyOffice_ACPD acpd);
        Task<string> READ();
        Task<string> READ(string sid);
        Task<bool> Update(string sid, MyOffice_ACPD acpd);
        //Task<bool> Delete(string sid);
    }
}
