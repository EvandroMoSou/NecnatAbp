using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace NecnatAbp.Pages;

public class IndexModel : NecnatAbpPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
