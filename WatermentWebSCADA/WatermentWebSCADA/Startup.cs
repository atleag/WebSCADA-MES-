using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WatermentWebSCADA.Startup))]
namespace WatermentWebSCADA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
