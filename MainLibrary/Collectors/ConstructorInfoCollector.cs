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

        public List<ParameterInfo> Parametrs { get; }

        public List<string> GenericTypes { get; }

        public ConstructorInfoCollector(ConstructorInfo constructor)
        {
            Modificator = constructor.IsPublic ? "public " : "non-public ";
            Static = constructor.IsStatic ? "static " : "";
            Name = TypesConverter.ChangeTypeName(constructor.DeclaringType.Name) + " ";
            Parametrs = constructor.GetParameters().ToList();
            GenericTypes = new List<string>();//constructor.GetGenericArguments().Select(g => g.Name).ToList();
        }

        public override string ToString()
        {
            var result = "CONSTRUCTOR: ";
            result += Modificator + Static + Name;
            if (GenericTypes.Count != 0)
            {
                result += "<";
                foreach (var type in GenericTypes)
                {
                    result += type + ",";
                }
                result= result.Remove(result.Length - 1);
                result += ">";
            }
            result += "( ";

            foreach (var param in Parametrs)
            {
                result += $"{TypesConverter.ChangeTypeName(param.ParameterType.Name)} {param.Name} ,";
            }
            result = result.Remove(result.Length - 1);
            result += ")";
            return result;

        }

    }
}
