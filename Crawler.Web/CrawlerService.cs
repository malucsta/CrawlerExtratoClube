using Crawler.Web.Builders;
using Crawler.Web.Enums;
using Crawler.Web.Factories.Steps.Pages.ExtratoClube;
using Crawler.Web.Factories;
using OpenQA.Selenium;

namespace Crawler.Web;

public class CrawlerService
{
    public static IEnumerable<string> CrawlExtratoClubeEnrollments(string user, string password, string cpf)
    {
        var chromedriverPath = GetChromeDriverPath();
        var driver = CrawlerFactory.CreateWebDriver(BrowserType.Chrome, chromedriverPath);

        driver.Url = "http://extratoclube.com.br/";

        IWebElement frameElement = driver.FindElement(By.TagName("frame"));
        driver.SwitchTo().Frame(frameElement);

        var steps = new WebDriverActionsBuilder(driver)
                    .AppendSteps(LoginAction.GetSteps(user, password))
                    .AppendSteps(ClosePopUpAction.GetSteps())
                    .AppendSteps(CloseMenuAction.GetSteps())
                    .AppendSteps(OpenCPFMenuAction.GetSteps())
                    .AppendSteps(SearchCPFAction.GetSteps(cpf));

        steps.Build();

        var matriculas = driver.FindElements(By.CssSelector("ion-label"));
        ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", matriculas.First());
        Thread.Sleep(1000);

        var matriculasColetadas = new List<string>();

        foreach (var mat in matriculas)
            matriculasColetadas.Add(mat.Text);

        driver.Quit();
        return matriculasColetadas;
    }

    private static string GetChromeDriverPath()
    {
        string currentFolderPath = Directory.GetCurrentDirectory();
        currentFolderPath = $"{currentFolderPath}\\Drivers";
        return Path.Combine(currentFolderPath, "chromedriver.exe");
    }
}
