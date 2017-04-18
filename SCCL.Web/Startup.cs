using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SCCL.Web.Startup))]
namespace SCCL.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
