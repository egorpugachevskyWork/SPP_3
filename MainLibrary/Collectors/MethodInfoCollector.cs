using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MainLibrary.Collectors
{
    public class MethodInfoCollector
    {
        public string Modificator { get; }

        public string ReturnTypeName { get; }

        public string Static { get; }

        public string Name { get; }

        public string Extension { get; }

        public ParameterInfo[] Parametrs { get; }

        public List<string> GenericTypes { get; }

        public MethodInfoCollector(MethodInfo method)
        {
            Modificator = method.IsPublic ? "public " : "non-public";
            ReturnTypeName = method.ReturnType.Name;
            GenericTypes = method.GetGenericArguments().Select(g => g.Name).ToList();
            Static = method.IsStatic ? "static" : "";
            Name = method.Name;
            Extension = method.IsDefined(typeof(ExtensionAttribute), true) ? "extension " : "";
            Parametrs = method.GetParameters();
        }
    }
}
