using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    public class Source : BaseEntity
    {
        public string SourceType { get; set; } = null!;

        public ICollection<Event> Events { get; set; } = null!;
    }
}
