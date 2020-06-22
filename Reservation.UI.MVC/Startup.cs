using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Reservation.UI.MVC.Startup))]
namespace Reservation.UI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
