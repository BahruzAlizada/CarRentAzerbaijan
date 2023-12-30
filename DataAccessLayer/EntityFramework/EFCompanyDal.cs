using Azure;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EntityFramework
{
    public class EFCompanyDal : ICompanyDal
    {
        private readonly UserManager<AppUser> userManager;
        public EFCompanyDal(UserManager<AppUser> userManager)
        {
            this.userManager=userManager;
        }

        public async Task Activity(int id)
        {
            AppUser company = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (company.IsDeactive)
                company.IsDeactive = false;
            else
                company.IsDeactive = true;

            await userManager.UpdateAsync(company);
        }

        public async Task<int> AllCompaniesCountAsync()
        {
            int count = await userManager.Users.Where(x => x.UserRole.Contains("Company")).CountAsync();
            return count;
        }

        public async Task<double> AllCompaniesPageCount(double take)
        {
            double count = await userManager.Users.Where(x => x.UserRole.Contains("Company")).CountAsync();
            double pageCount =Math.Ceiling(count/take);
            return pageCount;
        }

        public async Task Delete(int id)
        {
            AppUser company = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            await userManager.DeleteAsync(company);
        }

        public async Task<List<CompanyDto>> GetAllCompaniesWithPaging(int take, int page)
        {
            using var context = new Context();
            
            List<AppUser> companies = await userManager.Users.Where(x => x.UserRole.Contains("Company")).
                OrderByDescending(x => x.IsPremium).ThenByDescending(x => x.Cars.Count()).
                Skip((page - 1) * take).Take(take).Select(x=> new AppUser
                {
                    Id=x.Id,
                    Name=x.Name,
                    Email=x.Email,
                    UserName=x.UserName,
                    PhoneNumber=x.PhoneNumber,
                    Image=x.Image,
                    CompanyDescription=x.CompanyDescription,
                    Address=x.Address,
                    IsDeactive=x.IsDeactive,
                    IsPremium=x.IsPremium
                }).ToListAsync();

            List<CompanyDto> companyDtos = new List<CompanyDto>();

            foreach (var item in companies)
            {
                CompanyDto company = new CompanyDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    UserName=item.UserName,
                    Phone = item.PhoneNumber,
                    Image = item.Image,
                    CompanyDescription = item.CompanyDescription,
                    Address = item.Address,
                    IsDeactive = item.IsDeactive,
                    IsPremium = item.IsPremium,
                    CarCount=await context.Cars.Where(x=>!x.IsDeactive && x.UserId==item.Id).CountAsync(),
                    PremiumCarCount=await context.Cars.Where(x => !x.IsDeactive && x.IsPremium && x.UserId == item.Id).CountAsync()
                };
                companyDtos.Add(company);
            }

            return companyDtos;
        }

        public async Task<CompanyDto> GetCompanyById(int? id)
        {
            AppUser? company = await userManager.Users.Where(x => x.UserRole.Contains("Company")).
                Select(x => new AppUser
                {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Image = x.Image,
                    CompanyDescription = x.CompanyDescription,
                    Address = x.Address,
                    IsDeactive = x.IsDeactive,
                    IsPremium = x.IsPremium
                }).FirstOrDefaultAsync(x=>x.Id==id);


            CompanyDto companyDto = new CompanyDto
            {
                Id = company.Id,
                Name = company.Name,
                Email = company.Email,
                Phone = company.PhoneNumber,
                Image = company.Image,
                CompanyDescription = company.CompanyDescription,
                Address = company.Address,
                IsDeactive = company.IsDeactive,
                IsPremium = company.IsPremium
            };


            return companyDto;
        }

        public async Task<List<CompanyDto>> GetPremiumCompaniesWithPaging(int take, int page)
        {
            using var context = new Context();

            List<AppUser> companies = await userManager.Users.Where(x => x.UserRole.Contains("Company") && x.IsPremium).
                OrderByDescending(x => x.IsPremium).ThenByDescending(x => x.Cars.Count()).
                Skip((page - 1) * take).Take(take).Select(x => new AppUser
                {
                    Id = x.Id,
                    Name = x.Name,
                    UserName=x.UserName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    Image = x.Image,
                    CompanyDescription = x.CompanyDescription,
                    Address = x.Address,
                    IsDeactive = x.IsDeactive,
                    IsPremium = x.IsPremium
                }).ToListAsync();

            List<CompanyDto> companyDtos = new List<CompanyDto>();

            foreach (var item in companies)
            {
                CompanyDto company = new CompanyDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    UserName=item.UserName,
                    Phone = item.PhoneNumber,
                    Image = item.Image,
                    CompanyDescription = item.CompanyDescription,
                    Address = item.Address,
                    IsDeactive = item.IsDeactive,
                    IsPremium = item.IsPremium,
                    CarCount = await context.Cars.Where(x => !x.IsDeactive && x.UserId == item.Id).CountAsync(),
                    PremiumCarCount = await context.Cars.Where(x => !x.IsDeactive && x.IsPremium && x.UserId == item.Id).CountAsync()
                };
                companyDtos.Add(company);
            }

            return companyDtos;
        }

        public async Task Premium(int id)
        {
            AppUser company = await userManager.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (company.IsPremium)
                company.IsPremium = false;
            else
                company.IsPremium = true;

            await userManager.UpdateAsync(company);
        }

        public async Task<int> PremiumCompaniesForAdminPanelCountAsync()
        {
            int count = await userManager.Users.Where(x => x.UserRole.Contains("Company") && x.IsPremium).CountAsync();
            return count;
        }

        public async Task<double> PremiumCompaniesPageCount(double take)
        {
            double count = await userManager.Users.Where(x => x.UserRole.Contains("Company") && x.IsPremium).CountAsync();
            double pageCount = Math.Ceiling(count / take);
            return pageCount;
        }
    }
}
