using Crawler.Web.Builders.Steps;
using OpenQA.Selenium;

namespace Crawler.Web.Builders;

public class WebDriverActionsBuilder
{
    private IWebDriver _driver;
    private List<IStep> _steps;

    public WebDriverActionsBuilder(IWebDriver driver)
    {
        _driver = driver;
        _steps = new List<IStep>();
    }

    public WebDriverActionsBuilder AddStep(IStep step)
    {
        _steps.Add(step);
        return this;
    }

    public WebDriverActionsBuilder AppendSteps(IEnumerable<IStep> steps)
    {
        _steps.AddRange(steps);
        return this;
    }

    public void Build()
    {
        foreach (var step in _steps)
        {
            step.Execute(_driver);
        }
    }
}
