using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class Device : BaseComponent
    {
        public List<Module> Modules { get; set; }

        public List<Component> Components { get; set; }

        public Device(string name)
        {
            Name = name;
            Modules = new List<Module>();
            Components = new List<Component>();
        }

        public Device(string name, long longProp, DateTime dateProp)
            : this(name)
        {
            LongProperty = longProp;
            DateProperty = dateProp;
        }
    }
}
