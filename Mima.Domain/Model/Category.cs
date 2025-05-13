using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mima.Domain.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
