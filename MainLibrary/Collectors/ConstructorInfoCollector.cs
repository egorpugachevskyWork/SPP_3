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

        public override string ToString()
        {
            var result = "";
            result = Modificator + Static + Name;
            if (GenericTypes.Count != 0)
            {
                result += "<";
                foreach (var type in GenericTypes)
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

            foreach (var param in Parametrs)
            {
                result += $"{param.ParameterType} {param.Name} ,";
            }
            result.Remove(result.Length - 1);
            result += ")";
            return result;

        }
    }
}
