using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EdgeRealEstate.Startup))]
namespace EdgeRealEstate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
