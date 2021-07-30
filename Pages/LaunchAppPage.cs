using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Query = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.AppQuery>;
using WebQuery = System.Func<Xamarin.UITest.Queries.AppQuery, Xamarin.UITest.Queries.InvokeJSAppQuery>;
namespace SpecFlowXamarinUITest.Pages
{
    public class LaunchAppPage : Hooks.BasePage
    {
        readonly Query OkButton;
        readonly WebQuery AddText;
        protected override Hooks.PlatformQuery Trait => new Hooks.PlatformQuery
        {
            Android = x => x.Marked("OK"),
            iOS = x => x.Marked("OK")
        };

        public LaunchAppPage()
        {
            if (OnAndroid)
            {
                OkButton = x => x.Marked("OK");
                AddText = x => x.WebView().InvokeJS("document.getElementById('firstname').value = 'Anvesh'");
            }

            if (OniOS)
            {
                OkButton = x => x.Marked("OK");
                AddText = x => x.WebView().InvokeJS("document.getElementById('firstname').value = 'Anvesh'");             }
        }

        public void ClickOk()
        {
            app.WaitForElement(OkButton);
            app.Screenshot("App Launch");
            app.Tap(OkButton);
        }

        public void AddTextIn()
        {
            app.Query(AddText);
            Thread.Sleep(5000);
            app.Screenshot("Entered text");
        }

        public void WaitForSec()
        {
            Thread.Sleep(5000);
            app.Screenshot("After click ok");
            
        }

    }
}
