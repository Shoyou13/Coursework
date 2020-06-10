using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GroupWorkDVD.Startup))]
namespace GroupWorkDVD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
