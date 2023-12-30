

using Core.DataAccess;
using EntityLayer.Dtos;
using System.Diagnostics;

namespace DataAccessLayer.Abstract
{
    public interface ICompanyDal
    {
        Task Activity(int id);
        Task Premium(int id);
        Task Delete(int id);

        Task<List<CompanyDto>> GetAllCompaniesWithPaging(int take, int page);
        Task<double> AllCompaniesPageCount(double take);
        Task<int> AllCompaniesCountAsync();


        Task<List<CompanyDto>> GetPremiumCompaniesWithPaging(int take, int page);
        Task<double> PremiumCompaniesPageCount(double take);
        Task<int> PremiumCompaniesForAdminPanelCountAsync();

        Task<CompanyDto> GetCompanyById(int? id);
    }
}
