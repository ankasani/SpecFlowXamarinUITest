using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Xamarin.UITest;

namespace SpecFlowXamarinUITest.Hooks
{
    [Binding]
    //[TestFixture(Platform.Android)]
    public class TestHooks
    {
        protected TestHooks()
        {
            AppManager.Platform = Platform.Android;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            AppManager.StartApp();
        }

        [AfterScenario]
        public void AfterScenario()
        {
            Console.Out.WriteLine("AfterScenario");
        }
    }
}
