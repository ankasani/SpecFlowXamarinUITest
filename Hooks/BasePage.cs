using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.UITest;

namespace SpecFlowXamarinUITest.Hooks
{
    public abstract class BasePage
    {
        protected IApp app => AppManager.App;
        protected bool OnAndroid => AppManager.Platform == Platform.Android;
        protected bool OniOS => AppManager.Platform == Platform.iOS;
        protected abstract PlatformQuery Trait { get; }

        protected BasePage()
        {
        }

        /// <summary>
        /// Verifies that the trait is present. Uses the default wait time.
        /// </summary>
        /// <param name="timeout">Time to wait before the assertion fails</param>
        public void AssertOnPage(TimeSpan? timeout = default(TimeSpan?))
        {
            var message = "Unable to verify on page: " + this.GetType().Name;

            Assert.DoesNotThrow(() => app.WaitForElement(Trait.Current, timeout: timeout), message);
        }
    }
}
