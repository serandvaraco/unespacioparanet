using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TodoItemPush.MobileAppService.Startup))]

namespace TodoItemPush.MobileAppService
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}