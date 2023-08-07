using OpenQA.Selenium;

namespace Crawler.Web.Builders.Steps;

public class ScrollToBottom : IStep
{
    private int _waitTime;

    public ScrollToBottom(int waitTime = 0)
    {
        _waitTime = waitTime;
    }

    public void Execute(IWebDriver driver)
    {
        ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
        Thread.Sleep(_waitTime);
    }
}
