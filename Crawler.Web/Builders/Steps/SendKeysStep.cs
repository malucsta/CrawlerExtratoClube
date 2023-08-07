using Crawler.Web.Enums;
using Crawler.Web.Factories;
using OpenQA.Selenium;

namespace Crawler.Web.Builders.Steps;

public class SendKeysStep : IStep
{
    private IWebElement element;
    private string _tag;
    private Selector _selector;
    private string _text;

    public SendKeysStep(string tag, Selector selector, string text)
    {
        _text = text;
        _tag = tag;
        _selector = selector;
    }

    public void Execute(IWebDriver driver)
    {
        IWebElementFactory.GetElement(_tag, _selector, driver).SendKeys(_text);
    }
}
