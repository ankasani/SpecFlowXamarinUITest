using System;
using TechTalk.SpecFlow;

namespace SpecFlowXamarinUITest.Steps
{
    [Binding]
    public class LaunchAppSteps
    {
        Pages.LaunchAppPage lap = new Pages.LaunchAppPage();

        [Given(@"Click On Ok Button")]
        public void GivenClickOnOkButton()
        {
            lap.ClickOk();
        }
        
        [Given(@"Again Click On Ok Button")]
        public void GivenAgainClickOnOkButton()
        {
            lap.ClickOk();
        }
        
        [When(@"Two Click Are Done")]
        public void WhenTwoClickAreDone()
        {
            lap.WaitForSec();
        }
        
        [Then(@"Page Need To Load with Video")]
        public void ThenPageNeedToLoadWithVideo()
        {
            lap.AddTextIn();
        }
    }
}
