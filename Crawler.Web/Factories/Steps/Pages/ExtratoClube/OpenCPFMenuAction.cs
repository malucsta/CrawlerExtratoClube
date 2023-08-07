using Crawler.Web.Builders.Steps;

namespace Crawler.Web.Factories.Steps.Pages.ExtratoClube;

public static class OpenCPFMenuAction
{
    public static List<IStep> GetSteps()
    {
        var tag = "#extratoonline > ion-row:nth-child(2) > ion-col > ion-card > ion-button:nth-child(17)";
        var firstTag = "#extratoonline > ion-row:nth-child(2) > ion-col > ion-card > ion-button:nth-child(5)";

        return new List<IStep>
    {
        new ScrollIntoViewStep(firstTag, Enums.Selector.CssSelector, 1000),
        new ScrollIntoViewStep(tag, Enums.Selector.CssSelector, 1000),
        new ClickStep(tag, Enums.Selector.CssSelector, 1000)
    };
    }
}
