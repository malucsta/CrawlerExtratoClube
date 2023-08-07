using Crawler.Web.Enums;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using Microsoft.Extensions.Options;
using OpenQA.Selenium.Interactions;

namespace Crawler.Web.Factories;

public static class CrawlerFactory
{
    public static IWebDriver CreateWebDriver(BrowserType browserType, string driverPath, bool headless = true)
    {
        switch (browserType)
        {
            case BrowserType.Chrome:
                var chromeOptions = new ChromeOptions();
                if (headless) chromeOptions.AddArgument("--headless");
                chromeOptions.AddArgument("--no-sandbox"); // Necessário para evitar problemas em contêineres

                ChromeDriverService service = ChromeDriverService.CreateDefaultService();
                var port = service.Port;
                service.Port = 9515;
                return new ChromeDriver(service, chromeOptions);

                // Create and return a new ChromeDriver instance
                //return new ChromeDriver(chromeOptions);
        

                ////chromeOptions.AddArgument("--incognito");
                //return new ChromeDriver(driverPath, chromeOptions);

            case BrowserType.Firefox:
                var firefoxOptions = new FirefoxOptions();
                if (headless) firefoxOptions.AddArgument("--headless");

                return new FirefoxDriver(driverPath, firefoxOptions);

            case BrowserType.Edge:
                var edgeOptions = new EdgeOptions();
                if (headless) edgeOptions.AddArgument("--headless");

                return new EdgeDriver(driverPath, edgeOptions);

            default:
                throw new NotSupportedException($"Browser type '{browserType}' is not supported.");
        }
    }
}
