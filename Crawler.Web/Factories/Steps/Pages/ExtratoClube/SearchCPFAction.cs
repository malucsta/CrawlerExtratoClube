using Crawler.Web.Builders.Steps;
using Crawler.Web.Enums;

namespace Crawler.Web.Factories.Steps.Pages.ExtratoClube;

public static class SearchCPFAction
{
    public static List<IStep> GetSteps(string cpf)
    {
        var inputTag = "ion-input-1";
        var buttonTag = "#extratoonline > ion-row:nth-child(2) > ion-col > ion-card > ion-grid > ion-row:nth-child(2) > ion-col > ion-card > ion-button";

        return new List<IStep>
    {
        new ScrollIntoViewStep(inputTag, Selector.Name, 1000),
        new SendKeysStep(inputTag, Selector.Name, cpf),
        new ClickStep(buttonTag, Selector.CssSelector, 2000)
    };
    }
}
