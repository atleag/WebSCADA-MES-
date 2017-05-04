namespace codingfreaks.samples.Identity.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;

    public class MyRole : IdentityRole<long, MyUserRole>
    {
        public MyRole() : base() { }

        //public MyRole(string name) : base(name) { }
        //nu
        public string Description { get; set; }
    }
}