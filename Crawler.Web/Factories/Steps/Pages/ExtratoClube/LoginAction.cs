using Crawler.Web.Builders.Steps;

namespace Crawler.Web.Factories.Steps.Pages.ExtratoClube;

public static class LoginAction
{
    public static List<IStep> GetSteps(string user, string password)
    {
        return new List<IStep>
    {
        new SendKeysStep("user", Enums.Selector.Id, user),
        new SendKeysStep("pass", Enums.Selector.Id, password),
        new ClickStep("botao", Enums.Selector.Id, 3000),
    };
    }
}
