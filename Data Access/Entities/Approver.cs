using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access.Entities
{
    internal class Approver : BaseEntity
    {
        public string ApproverType { get; set;}

        public ICollection<Event> Events { get; set;}
    }
}
