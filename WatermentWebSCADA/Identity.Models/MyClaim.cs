// Purpose: Enable ASP.NET Identity to support T4/DB-First and long / INT(11) as Primary Key.
// All credits for this custom project / framework class goes to codingfreaks.de 
// URL 1: http://www.codingfreaks.de/2014/01/11/microsoft-aspnet-identity-in-bestender-anwendung-einsetzen/
// URL 2: http://www.codingfreaks.de/2014/06/16/microsoft-aspnet-identity-in-bestehender-anwendung-einsetzen-teil-2/
// - Moskoskos

namespace codingfreaks.samples.Identity.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class MyClaim : IdentityUserClaim<long>
    {
    }
}
