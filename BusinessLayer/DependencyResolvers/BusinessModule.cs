using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace BusinessLayer.DependencyResolvers
{
    public static class BusinessModule
    {
        public static void BusinessLoad(this IServiceCollection services)
        {

            services.AddScoped<ICityService, CityManager>();
            services.AddScoped<ICityDal, EFCityDal>();


            //services.AddScoped<ICompanyService, CompanyManager>();
            //services.AddScoped<ICompanyDal, EFCompanyDal>();

            //services.AddScoped<IContactService, ContactManager>();
            //services.AddScoped<IContactDal, EFContactDal>();

            //services.AddScoped<IBannerService, BannerManager>();
            //services.AddScoped<IBannerDal, EFBannerDal>();

            //services.AddScoped<IAboutService, AboutManager>();
            //services.AddScoped<IAboutDal, EFAboutDal>();

            //services.AddScoped<IService, ServiceManager>();
            //services.AddScoped<IServiceDal, EFServiceDal>();

            //services.AddScoped<ISocialMediaService, SocialMediaManager>();
            //services.AddScoped<ISocialMediaDal,EFSocialMediaDal>();

            //services.AddScoped<IFaqService, FaqManager>();
            //services.AddScoped<IFaqDal, EFFaqDal>();
        }
    }
}
