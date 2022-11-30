using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Pet
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int HungerLevel { get; set; }

        public int ThirstyLevel { get; set; }

        [ForeignKey(nameof(Farm))]
        public Guid FarmId { get; set; }
        public Farm Farm { get; set; }
    }
}
