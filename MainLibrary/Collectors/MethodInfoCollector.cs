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

        public string Abstract { get; }

        public string Virtual { get; }

        public MethodInfoCollector(MethodInfo method)
        {
            Modificator = method.IsPublic ? "public " : "non-public ";
            ReturnTypeName = method.ReturnType.Name + " ";
            GenericTypes = method.GetGenericArguments().Select(g => g.Name).ToList();
            Static = method.IsStatic ? "static " : "";
            Name = method.Name + " ";
            Extension = method.IsDefined(typeof(ExtensionAttribute), true) ? "extension " : "";
            Parametrs = method.GetParameters();
            Abstract = method.IsAbstract ? "abstract " : "";
            Virtual = method.IsVirtual ? "virtual " : "";
        }

        public override string ToString()
        {
            var result = "";
            result = Extension + Modificator + Abstract + Virtual + Static + ReturnTypeName + Name;
            if (GenericTypes.Count != 0)
            {
                result += "<";
                foreach(var type in GenericTypes)
                {
                    result += type + ",";
                }
                result.Remove(result.Length - 1);
                result += ">(";
            }
            else
            {
                result += "(";
            }

            foreach(var param in Parametrs)
            {
                result += $"{param.ParameterType} {param.Name} ,";
            }
            result.Remove(result.Length - 1);
            result += ")";
            return result;

        }
    }
}
