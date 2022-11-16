using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.Collectors
{
    public class FieldInfoCollector
    {
        public string Modificator { get; }

        public string TypeName { get; }

        public string Static { get;  }

        public string Name { get; }

        public FieldInfoCollector(FieldInfo field)
        {
            Modificator = field.IsPublic ? "public " : "non-public ";
            TypeName = TypesConverter.ChangeTypeName(field.FieldType.Name) + " ";
            Static = field.IsStatic ? "static " : "";
            Name = field.Name + " ";
        }

        public override string ToString()
        {
            var result = "FIELD: ";
            return result + Modificator + Static + TypeName + Name;
        }

       
    }
}
