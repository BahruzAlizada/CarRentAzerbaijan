
using EntityLayer.Dtos;

namespace BusinessLayer.Abstract
{
    public interface ICompanyService
    {
        Task ActivityAsync(int id);
        Task PremiumAsync(int id);
        Task DeleteAsync(int id);


        Task<List<CompanyDto>> GetAllCompaniesWithPagingAsync(int take, int page);
        Task<double> AllCompaniesPageCountAsync(double take);
        Task<int> AllCompaniesCountAsync();


        Task<List<CompanyDto>> GetPremiumCompaniesWithPagingAsync(int take, int page);
        Task<double> PremiumCompaniesPageCountAsync(double take);
        Task<int> PremiumCompaniesForAdminPanelCountAsync();


        Task<CompanyDto> GetCompanyByIdAsync(int? id);
    }
}
