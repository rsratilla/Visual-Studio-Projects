using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sayts.Startup))]
namespace Sayts
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
