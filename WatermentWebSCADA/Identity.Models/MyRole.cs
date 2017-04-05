namespace codingfreaks.samples.Identity.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class MyRole : IdentityRole<long, MyUserRole>
    {
    }
}
