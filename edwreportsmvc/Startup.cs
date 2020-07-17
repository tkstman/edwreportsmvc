using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(edwreportsmvc.Startup))]
namespace edwreportsmvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
