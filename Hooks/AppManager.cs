using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace SpecFlowXamarinUITest.Hooks
{
    static class AppManager
    {
        static IApp app;
        public static IApp App
        {
            get
            {
                if (app == null)
                    throw new NullReferenceException("'AppManager.App' not set. Call 'AppManager.StartApp()' before trying to access it.");
                return app;
            }
        }

        static Platform? platform = Platform.Android;
        public static Platform Platform
        {
            get
            {
                if (platform == null)
                    throw new NullReferenceException("'AppManager.Platform' not set.");
                return platform.Value;
            }

            set
            {
                platform = value;
            }
        }

        public static void StartApp()
        {
            if (Platform == Platform.Android)
            {
                app = ConfigureApp
                    .Android
                    // Used to run a .apk file:
                    .ApkFile("K:\\VSAC\\WorkingWithWebview.Android.apk")
                    .StartApp();
            }

            if (Platform == Platform.iOS)
            {
                app = ConfigureApp
                    .iOS
                    .StartApp();
            }
        }
    }
}
