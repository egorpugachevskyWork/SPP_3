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

        public List<ParameterInfo> Parametrs { get; }

        public List<string> GenericTypes { get; }

        public string Abstract { get; }


        public MethodInfoCollector(MethodInfo method)
        {
            Modificator = method.IsPublic ? "public " : "non-public ";
            ReturnTypeName = TypesConverter.ChangeTypeName(method.ReturnType.Name) + " ";
            GenericTypes = method.GetGenericArguments().Select(g => g.Name).ToList();
            Static = method.IsStatic ? "static " : "";
            Name = method.Name + " ";
            Extension = method.IsDefined(typeof(ExtensionAttribute), true) ? "extension " : "";
            Parametrs = method.GetParameters().ToList();
            Abstract = method.IsAbstract ? "abstract " : "";
        }

        public override string ToString()
        {
            var result = "METHOD: ";
            result += Extension + Modificator + Abstract + Static + ReturnTypeName + Name;
            if (GenericTypes.Count != 0)
            {
                result += "<";
                foreach(var type in GenericTypes)
                {
                    result += type + ",";
                }
                result = result.Remove(result.Length - 1);
                result += ">";
            }

            result += "( ";

            foreach(var param in Parametrs)
            {
                result += $"{TypesConverter.ChangeTypeName(param.ParameterType.Name)} {param.Name} ,";
            }
            result = result.Remove(result.Length - 1);
            result += ")";
            return result;

        }

    }
}
