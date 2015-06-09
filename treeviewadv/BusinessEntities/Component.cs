using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessEntities
{
    public class Component : BaseComponent
    {
        public Component(string name)
        {
            Name = name;
        }

        public Component(string name, long longProp, DateTime dateProp)
            : this(name)
        {
            LongProperty = longProp;
            DateProperty = dateProp;
        }
    }
}
