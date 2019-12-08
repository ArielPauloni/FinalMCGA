using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FinalMCGA.Startup))]
namespace FinalMCGA
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
