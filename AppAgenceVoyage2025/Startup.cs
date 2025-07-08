using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppAgenceVoyage2025.Startup))]
namespace AppAgenceVoyage2025
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
