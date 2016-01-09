using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AUSKF.Startup))]
namespace AUSKF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
