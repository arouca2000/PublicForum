using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PublicForum.Startup))]
namespace PublicForum
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
