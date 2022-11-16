using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.Collectors
{
    public class ConstructorInfoCollector
    {
        public string Modificator { get; }

        public string Static { get; }

        public string Name { get; }

        public ParameterInfo[] Parametrs { get; }

        public List<string> GenericTypes { get; }

        public ConstructorInfoCollector(ConstructorInfo constructor)
        {
            Modificator = constructor.IsPublic ? "public " : "non-public";
            Static = constructor.IsStatic ? "static" : "";
            Name = constructor.DeclaringType.Name;
            Parametrs = constructor.GetParameters();
            GenericTypes = constructor.GetGenericArguments().Select(g => g.Name).ToList();
        }
    }
}
