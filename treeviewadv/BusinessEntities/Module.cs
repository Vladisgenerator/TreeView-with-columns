using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class Module : BaseComponent
    {
        public List<Module> Modules { get; set; }

        public List<Component> Components { get; set; }

        public Module(string name)
        {
            Name = name;
            Modules = new List<Module>();
            Components = new List<Component>();
        }

        public Module(string name, long longProp, DateTime dateProp)
            : this(name)
        {
            LongProperty = longProp;
            DateProperty = dateProp;
        }
    }
}
