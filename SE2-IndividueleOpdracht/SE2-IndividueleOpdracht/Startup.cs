using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SE2_IndividueleOpdracht.Startup))]
namespace SE2_IndividueleOpdracht
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
