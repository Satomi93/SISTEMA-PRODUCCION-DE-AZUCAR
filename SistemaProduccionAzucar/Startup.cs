using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaProduccionAzucar.Startup))]
namespace SistemaProduccionAzucar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
