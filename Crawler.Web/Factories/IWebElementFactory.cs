using Crawler.Web.Enums;
using OpenQA.Selenium;

namespace Crawler.Web.Factories;

public class IWebElementFactory
{
    public static IWebElement GetElement(string tag, Selector selector, IWebDriver driver)
    {
        By? by = selector switch
        {
            Selector.Name => By.Name(tag),
            Selector.Id => By.Id(tag),
            Selector.CssSelector => By.CssSelector(tag),
            _ => throw new ArgumentException("Selector not supported"),
        };

        return driver.FindElement(by);
    }
}
