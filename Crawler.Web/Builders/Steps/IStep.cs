using OpenQA.Selenium;

namespace Crawler.Web.Builders.Steps;

public interface IStep
{
    void Execute(IWebDriver driver);
}
