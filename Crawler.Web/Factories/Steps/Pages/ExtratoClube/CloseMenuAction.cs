using Crawler.Web.Builders.Steps;

namespace Crawler.Web.Factories.Steps.Pages.ExtratoClube;

public static class CloseMenuAction
{
    public static List<IStep> GetSteps()
    {
        var tag = "body > app-root > app-home > ion-app > ion-menu > ion-content > ion-list > ion-item:nth-child(2)";

        return new List<IStep>
        {
            new ClickStep(tag, Enums.Selector.CssSelector, 1000),
        };
    }
}
