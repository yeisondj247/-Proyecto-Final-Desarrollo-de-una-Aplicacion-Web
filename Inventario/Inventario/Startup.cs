using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof( Noodle.Startup))]
namespace Noodle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
