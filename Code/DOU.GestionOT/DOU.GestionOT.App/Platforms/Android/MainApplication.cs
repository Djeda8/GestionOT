using Android.App;
using Android.Runtime;

namespace DOU.GestionOT.App
{
    //[Application(UsesCleartextTraffic = true)]
#if DEBUG 
    [Application(UsesCleartextTraffic = true)] 
    // for development
#else 
    [Application(UsesCleartextTraffic = false)] 
    // production 
#endif
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
    }
}
