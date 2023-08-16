using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    public class EventSource : BaseEntity
    {
        public string SourceType { get; set; } = null!;

        public ICollection<NaturalDisasterEvent> Events { get; set; } = null!;
    }
}
