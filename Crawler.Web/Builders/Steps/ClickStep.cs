using Crawler.Web.Enums;
using Crawler.Web.Factories;
using OpenQA.Selenium;

namespace Crawler.Web.Builders.Steps;

public class ClickStep : IStep
{
    private string _tag;
    private Selector _selector;
    private int _waitTime;

    public ClickStep(string tag, Selector selector, int waitTime = 0)
    {
        _tag = tag;
        _selector = selector;
        _waitTime = waitTime;
    }

    public void Execute(IWebDriver driver)
    {
        IWebElementFactory.GetElement(_tag, _selector, driver).Click();
        Thread.Sleep(_waitTime);
    }
}
