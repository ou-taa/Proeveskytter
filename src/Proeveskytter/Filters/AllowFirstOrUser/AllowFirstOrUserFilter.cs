using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Proeveskytter.Filters.AllowFirstOrUser
{
    /// <summary>
    /// Tillader anonym adgang, hvis der ikke er oprettet nogen brugere endnu (første kørsel), 
    /// ellers kræver det, at brugeren er logget ind.
    /// </summary>
    public class AllowFirstOrUserFilter : IAsyncAuthorizationFilter
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AllowFirstOrUserFilter(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            bool hasAnyUsers = await _userManager.Users.AnyAsync();

            // First-run mode: no users yet, allow anonymous access
            if (!hasAnyUsers)
                return;

            var user = context.HttpContext.User;

            // After bootstrap: require login
            if (user?.Identity?.IsAuthenticated != true)
            {
                context.Result = new ChallengeResult();
                return;
            }
            return;
        }
    }
}