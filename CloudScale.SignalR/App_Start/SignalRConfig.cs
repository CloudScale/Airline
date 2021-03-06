using Microsoft.Owin.Cors;
using Owin;

namespace CloudScale.SignalR
{
    public static class SignalRConfig
    {
        public static void Register(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
}