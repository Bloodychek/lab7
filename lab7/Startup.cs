using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(lab7.Startup))]
namespace lab7
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
