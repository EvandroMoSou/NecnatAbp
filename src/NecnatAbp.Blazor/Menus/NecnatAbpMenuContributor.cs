using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace NecnatAbp.Blazor.Menus;

public class NecnatAbpMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(NecnatAbpMenus.Prefix, displayName: "NecnatAbp", "/NecnatAbp", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
