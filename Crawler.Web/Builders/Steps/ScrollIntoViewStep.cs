using Crawler.Web.Enums;
using Crawler.Web.Factories;
using OpenQA.Selenium;

namespace Crawler.Web.Builders.Steps;

public class ScrollIntoViewStep : IStep
{
    private string _tag;
    private Selector _selector;
    private int _waitTime;

    public ScrollIntoViewStep(string tag, Selector selector, int waitTime = 0)
    {
        _tag = tag;
        _waitTime = waitTime;
        _selector = selector;
    }

    public void Execute(IWebDriver driver)
    {
        var element = IWebElementFactory.GetElement(_tag, _selector, driver);
        ((IJavaScriptExecutor)driver).ExecuteScript($"arguments[0].scrollIntoView(true);", element);
        Thread.Sleep(_waitTime);
    }
}
