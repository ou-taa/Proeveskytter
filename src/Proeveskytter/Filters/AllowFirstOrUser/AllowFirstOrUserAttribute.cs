using Microsoft.AspNetCore.Mvc;

namespace Proeveskytter.Filters.AllowFirstOrUser
{
    /// <summary>
    /// Tillader anonym adgang, hvis der ikke er oprettet nogen brugere endnu (første kørsel), 
    /// ellers kræver det, at brugeren er logget ind.
    /// </summary>
    public class AllowFirstOrUserAttribute : TypeFilterAttribute
    {
        public AllowFirstOrUserAttribute() : base(typeof(AllowFirstOrUserFilter))
        {
        }
    }
}
