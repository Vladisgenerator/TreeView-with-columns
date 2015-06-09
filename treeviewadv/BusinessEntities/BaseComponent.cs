using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public abstract class BaseComponent
    {
        public string Name { get; set; }

        public long LongProperty { get; set; }

        public DateTime DateProperty { get; set; }
    }
}
