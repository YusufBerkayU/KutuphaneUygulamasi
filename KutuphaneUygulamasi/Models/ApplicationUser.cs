using Microsoft.AspNetCore.Identity;

namespace KutuphaneUygulamasi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Email {  get; set; }  
        //public string Password { get; set; }    
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
