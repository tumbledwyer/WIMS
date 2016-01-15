using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WhereIsMyShit.Startup))]
namespace WhereIsMyShit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
