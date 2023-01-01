using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class FarmForUpdateDto
    {
        public string Name { get; set; }

        public IEnumerable<PetForCreationDto> Pets { get; set; }
    }
}
