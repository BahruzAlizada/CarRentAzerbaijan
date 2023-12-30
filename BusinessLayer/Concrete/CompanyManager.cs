using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Dtos;

namespace BusinessLayer.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyDal companyDal;
        public CompanyManager(ICompanyDal companyDal)
        {
            this.companyDal = companyDal;
        }

        public async Task ActivityAsync(int id)
        {
            await companyDal.Activity(id);
        }

        public async Task<int> AllCompaniesCountAsync()
        {
            return await companyDal.AllCompaniesCountAsync();
        }

        public async Task<double> AllCompaniesPageCountAsync(double take)
        {
            return await companyDal.AllCompaniesPageCount(take);
        }

        public async Task DeleteAsync(int id)
        {
            await companyDal.Delete(id);
        }

        public async Task<List<CompanyDto>> GetAllCompaniesWithPagingAsync(int take, int page)
        {
            return await companyDal.GetAllCompaniesWithPaging(take, page);
        }

        public async Task<CompanyDto> GetCompanyByIdAsync(int? id)
        {
            return await companyDal.GetCompanyById(id);
        }

        public async Task<List<CompanyDto>> GetPremiumCompaniesWithPagingAsync(int take, int page)
        {
            return await companyDal.GetPremiumCompaniesWithPaging(take, page);
        }

        public async Task PremiumAsync(int id)
        {
            await companyDal.Premium(id);
        }

        public async Task<int> PremiumCompaniesForAdminPanelCountAsync()
        {
            return await companyDal.PremiumCompaniesForAdminPanelCountAsync();
        }

        public async Task<double> PremiumCompaniesPageCountAsync(double take)
        {
            return await companyDal.PremiumCompaniesPageCount(take);
        }
    }
}
