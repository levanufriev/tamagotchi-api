using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Farm
    {
        public Guid Id { get; set; }

        public ICollection<Pet> Pets { get; set; }
    }
}
