using Microsoft.AspNetCore.Identity;

namespace Mima.Domain.Model
{
    public class User : IdentityUser
    {
        public ICollection<Product> Products { get; set; }
        public ICollection<Category> Categories { get; set; }

    }
}
