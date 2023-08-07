using Crawler.Web.Builders.Steps;

namespace Crawler.Web.Factories.Steps.Pages.ExtratoClube;

public static class ClosePopUpAction
{
    public static List<IStep> GetSteps()
    {
        var tag = "ion-button[title='Fechar'][color='success']";

        return new List<IStep>
    {
        new ClickStep(tag, Enums.Selector.CssSelector, 1000),
    };
    }
}
