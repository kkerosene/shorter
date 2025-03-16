using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace shorter.client.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Urls = new List<Url>();
        }

        public virtual ICollection<Url> Urls { get; set; }
    }
}