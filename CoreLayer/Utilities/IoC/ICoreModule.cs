using Microsoft.Extensions.DependencyInjection;


namespace CoreLayer.Utilities.IoC
{
    public interface ICoreModule
    {
        public void Load(IServiceCollection services);
    }
}
