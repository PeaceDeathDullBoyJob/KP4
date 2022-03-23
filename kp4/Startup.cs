using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(kp4.Startup))]
namespace kp4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
