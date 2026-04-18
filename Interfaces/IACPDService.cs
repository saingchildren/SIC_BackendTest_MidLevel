using MercuryTest.Models;

namespace MercuryTest.Interfaces
{
    public interface IACPDService
    {
        Task<bool> Create(MyOffice_ACPD acpd);
        Task<string> READ();
        //Task<bool> Update(MyOffice_ACPD acpd);
        //Task<bool> Delete(string sid);
    }
}
