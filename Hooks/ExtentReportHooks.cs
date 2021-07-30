using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using System;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;
using Xamarin.UITest;

namespace SpecFlowXamarinUITest.Hooks
{
	[Binding]
	public class ExtentReportHooks
	{
		private static ExtentTest featureName;
		private static ExtentTest scenario;
		private static ExtentReports extent;
		public static string ReportPath;
		public static bool takeScreenshotForFailure = true;
		public static bool takeScreenshotForSuccess = true;

		[BeforeTestRun]
		public static void BeforeTestRun()
		{
			Console.WriteLine("Before Test run starts");
			ClearData(GetDirectoryPath("ExtentReport"));
			CreateIfMissing(GetDirectoryPath("ExtentReport"));
			ExtentHtmlReporter extentHtmlReporter = new ExtentHtmlReporter(GetDirectoryPath("ExtentReport") + "\\index.html");
			extentHtmlReporter.Configuration().Theme = Theme.Standard;
			extent = new ExtentReports();
			extent.AttachReporter(extentHtmlReporter);
			Console.WriteLine("Before Test run ends");
		}

		[BeforeFeature]
		[Obsolete]
		public static void BeforeFeature(FeatureContext featureContext)
		{
			Console.WriteLine("Before Feature starts");
			featureName = extent.CreateTest<Feature>(FeatureContext.Current.FeatureInfo.Title);
			Console.WriteLine("Before Feature ends");
		}

		[BeforeScenario]
		[Obsolete]
		public static void BeforeScenarioStart(ScenarioContext scenarioContext)
		{
			Console.WriteLine("Before Scenario starts");
			scenario = featureName.CreateNode<Scenario>(ScenarioContext.Current.ScenarioInfo.Title);
			AppManager.StartApp();
			Console.WriteLine("Before Scenario ends");
		}

		[AfterStep]
		[Obsolete]
		public void AfterEachScenarioStep()
		{
			Console.WriteLine("After each step starts");
			string a = ScenarioStepContext.Current.StepInfo.StepDefinitionType.ToString();
			MediaEntityModelProvider provider = CaptureScreenshotAndReturnModel(ScenarioContext.Current.ScenarioInfo.Title.Trim());
			if (ScenarioContext.Current.TestError == null)
			{
				if (a == "Given")
				{
					scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Pass("", provider);
				}
				else if (a == "When")
				{
					scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Pass("", provider);
				}
				else if (a == "Then")
				{
					scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Pass("", provider);
				}
				else if (a == "And")
				{
					scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Pass("", provider);
				}
				else if (a == "But")
				{
				scenario.CreateNode<But>(ScenarioStepContext.Current.StepInfo.Text).Pass("", provider);
				}
			}
			else if (ScenarioContext.Current.TestError != null)
			{
				if (a == "Given")
				{
					scenario.CreateNode<Given>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message).Fail("\n" + ScenarioContext.Current.TestError.StackTrace, provider);
				}
				else if (a == "When")
				{
					scenario.CreateNode<When>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message).Fail("\n" + ScenarioContext.Current.TestError.StackTrace, provider);
				}
				else if (a == "Then")
				{
					scenario.CreateNode<Then>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message).Fail("\n" + ScenarioContext.Current.TestError.StackTrace, provider);
				}
				else if (a == "And")
				{
					scenario.CreateNode<And>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message).Fail("\n" + ScenarioContext.Current.TestError.StackTrace, provider);
				}
				else if (a == "But")
				{
				scenario.CreateNode<But>(ScenarioStepContext.Current.StepInfo.Text).Fail(ScenarioContext.Current.TestError.Message).Fail("\n" + ScenarioContext.Current.TestError.StackTrace, provider);
				}
			}
			Console.WriteLine("After each step ends");
		}

		[AfterTestRun]
		public static void AfterTestRun()
		{
			Console.WriteLine("After test run starts");
			extent.Flush();
			Console.WriteLine("After test run ends");
		}

		[AfterScenario]
		public static void AfterScenario()
		{
			Console.WriteLine("After Scenario");
		}

		[Obsolete]
		private static MediaEntityModelProvider CaptureScreenshotAndReturnModel(string name)
		{
			CreateIfMissing(GetDirectoryPath("ExtentReport\\Screenshots"));
			return null;
		}

		private static double GetCurrentMilliSeconds()
		{
			DateTime d = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
			return (DateTime.UtcNow - d).TotalMilliseconds;
		}

		private static string GetDirectoryPath(string folderName)
		{
			string str = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin\\Debug\\net472", "");
			return str + folderName;
		}

		private static void CreateIfMissing(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}

		private static void ClearData(string reportPath)
		{
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(reportPath);
				directoryInfo.EnumerateFiles().ToList().ForEach(delegate (FileInfo f)
				{
					f.Delete();
				});
				directoryInfo.EnumerateDirectories().ToList().ForEach(delegate (DirectoryInfo d)
				{
					d.EnumerateFiles().ToList().ForEach(delegate (FileInfo f)
					{
						f.Delete();
					});
				});
			}
			catch (Exception value)
			{
				Console.WriteLine(value);
			}
		}
	}
}
