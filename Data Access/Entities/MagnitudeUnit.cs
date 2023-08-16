using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    public class MagnitudeUnit : BaseEntity
    {
        public string MagnitudeUnitName { get; set; } = null!;

        public ICollection<NaturalDisasterEvent> Events { get; set; } = null!;
    }
}
