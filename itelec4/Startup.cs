using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(itelec4.Startup))]
namespace itelec4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
